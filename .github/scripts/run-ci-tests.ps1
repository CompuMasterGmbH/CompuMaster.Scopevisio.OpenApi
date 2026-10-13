param(
    [Parameter(Mandatory = $true)][ValidateSet('Windows', 'Linux', 'macOS')][string]$RunnerOs,
    [Parameter(Mandatory = $true)][ValidateSet('net8.0', 'net48')][string]$Framework,
    [string]$Configuration = 'Debug',
    [Parameter(Mandatory = $true)][string]$ResultsDirectory,
    [switch]$IncludeCredentialedSuite
)

$ErrorActionPreference = 'Stop'
$selection = & (Join-Path $PSScriptRoot 'get-ci-test-selection.ps1') -RunnerOs $RunnerOs -Framework $Framework
if ($IncludeCredentialedSuite -and $env:GITHUB_ACTIONS -ne 'true') {
    throw 'The credentialed suite is CI-only and still requires coordinated exclusive account access.'
}
Write-Host "OpenScope $RunnerOs / $Framework coverage=$($selection.Coverage)."
$runs = @()
if ($IncludeCredentialedSuite) {
    $runs += [pscustomobject]@{ Name = 'historical'; Project = 'src/CompuMaster.Scopevisio.OpenApi.Test.NetStandard/CompuMaster.Scopevisio.OpenApi.Test.NetStandard.csproj'; Filter = $selection.LegacyFilter; ExpectedTests = $selection.LegacyTests; ExpectedFixtures = @() }
}
$runs += [pscustomobject]@{ Name = 'isolated'; Project = 'src/CompuMaster.Scopevisio.OpenApi.AsyncTests/CompuMaster.Scopevisio.OpenApi.AsyncTests.csproj'; Filter = $selection.IsolatedFilter; ExpectedTests = @(); ExpectedFixtures = $selection.IsolatedFixtures }
foreach ($run in $runs) {
    $directory = Join-Path $ResultsDirectory $run.Name
    $report = Join-Path $directory 'results.trx'
    if (Test-Path -LiteralPath $report) { throw 'Use a fresh results directory; stale CI test evidence must not be reused.' }
    $arguments = @('test', $run.Project, '--framework', $Framework, '--configuration', $Configuration,
        '--no-restore', '--results-directory', $directory, '--logger', 'trx;LogFileName=results.trx', '--logger', 'console;verbosity=quiet')
    if ($run.Name -eq 'historical') { $arguments += @('--logger', 'junit') }
    if ($run.Filter) { $arguments += @('--filter', $run.Filter) }
    Write-Host ("Selected $($run.Name) coverage=$($selection.Coverage); filter=" + $(if ($run.Filter) { $run.Filter } else { '<all>' }))
    & dotnet @arguments
    if ($LASTEXITCODE -ne 0) { throw 'The selected CI test run failed.' }
    & (Join-Path $PSScriptRoot 'assert-ci-test-results.ps1') -Path $report -ExpectedTests $run.ExpectedTests -ExpectedFixtures $run.ExpectedFixtures
}
if ($env:GITHUB_STEP_SUMMARY) {
    Add-Content -LiteralPath $env:GITHUB_STEP_SUMMARY -Value "OpenScope $RunnerOs / ${Framework}: $($selection.Coverage) coverage; historical suite included=$IncludeCredentialedSuite. Samples do not establish complete coverage on this platform/runtime."
}
