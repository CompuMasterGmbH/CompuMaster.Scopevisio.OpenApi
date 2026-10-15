param(
    [Parameter(Mandatory = $true)][ValidateSet('Windows', 'Linux', 'macOS')][string]$RunnerOs,
    [Parameter(Mandatory = $true)][ValidateSet('net8.0', 'net48')][string]$Framework,
    [string]$Configuration = 'Debug',
    [Parameter(Mandatory = $true)][string]$ResultsDirectory,
    [switch]$IncludeCredentialedSuite
)

$ErrorActionPreference = 'Stop'
$selection = & (Join-Path $PSScriptRoot 'get-ci-test-selection.ps1') -RunnerOs $RunnerOs -Framework $Framework
if ($IncludeCredentialedSuite -and $selection.IncludeHistorical -and $env:GITHUB_ACTIONS -ne 'true') {
    throw 'The credentialed suite is CI-only and still requires coordinated exclusive account access.'
}
Write-Host "OpenScope $RunnerOs / $Framework coverage=$($selection.Coverage)."
$runs = @()
if ($IncludeCredentialedSuite -and $selection.IncludeHistorical) {
    $runs += [pscustomobject]@{ Name = 'historical'; Project = 'src/CompuMaster.Scopevisio.OpenApi.Test.NetStandard/CompuMaster.Scopevisio.OpenApi.Test.NetStandard.csproj'; Filter = $selection.LegacyFilter; ExpectedTests = $selection.LegacyTests; ExpectedFixtures = @() }
}
$runs += [pscustomobject]@{ Name = 'isolated'; Project = 'src/CompuMaster.Scopevisio.OpenApi.AsyncTests/CompuMaster.Scopevisio.OpenApi.AsyncTests.csproj'; Filter = $selection.IsolatedFilter; ExpectedTests = $selection.IsolatedTests; ExpectedFixtures = @() }
foreach ($run in $runs) {
    $directory = Join-Path $ResultsDirectory $run.Name
    $report = Join-Path $directory 'results.trx'
    if (Test-Path -LiteralPath $report) { throw 'Use a fresh results directory; stale CI test evidence must not be reused.' }
    if ($selection.MaximumCases -gt 0) {
        & dotnet build $run.Project '--framework' $Framework '--configuration' $Configuration '--no-restore' '--verbosity' 'quiet'
        if ($LASTEXITCODE -ne 0) { throw 'Sample build failed before discovery.' }
        $null = New-Item -ItemType Directory -Path $directory -Force
        $discovery = Join-Path $directory 'selected-tests.txt'
        $discovered = & dotnet test $run.Project '--framework' $Framework '--configuration' $Configuration '--no-build' '--no-restore' '--list-tests' '--filter' $run.Filter '--verbosity' 'quiet' 2>&1
        $discoveryExit = $LASTEXITCODE
        $discovered | Set-Content -LiteralPath $discovery -Encoding utf8
        if ($discoveryExit -ne 0) { throw 'Selected sample discovery failed before execution.' }
        & (Join-Path $PSScriptRoot 'assert-ci-test-discovery.ps1') -Path $discovery -ExpectedTests $run.ExpectedTests -MaximumCases $selection.MaximumCases
    }
    $arguments = @('test', $run.Project, '--framework', $Framework, '--configuration', $Configuration,
        '--no-restore', '--results-directory', $directory, '--logger', 'trx;LogFileName=results.trx', '--logger', 'console;verbosity=quiet')
    if ($run.Name -eq 'historical') { $arguments += @('--logger', 'junit') }
    if ($selection.MaximumCases -gt 0) { $arguments += '--no-build' }
    if ($run.Filter) { $arguments += @('--filter', $run.Filter) }
    Write-Host ("Selected $($run.Name) coverage=$($selection.Coverage); filter=" + $(if ($run.Filter) { $run.Filter } else { '<all>' }))
    & dotnet @arguments
    if ($LASTEXITCODE -ne 0) { throw 'The selected CI test run failed.' }
    & (Join-Path $PSScriptRoot 'assert-ci-test-results.ps1') -Path $report -ExpectedTests $run.ExpectedTests -ExpectedFixtures $run.ExpectedFixtures -MaximumCases $selection.MaximumCases
}
if ($env:GITHUB_STEP_SUMMARY) {
    $historicalIncluded = [bool]($IncludeCredentialedSuite -and $selection.IncludeHistorical)
    Add-Content -LiteralPath $env:GITHUB_STEP_SUMMARY -Value "OpenScope $RunnerOs / ${Framework}: $($selection.Coverage) coverage; historical suite included=$historicalIncluded; maximum sample cases=$($selection.MaximumCases). Samples do not establish complete coverage on this platform/runtime."
}
