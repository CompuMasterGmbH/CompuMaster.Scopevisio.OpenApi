param([Parameter(Mandatory = $true)][string]$Directory)

$ErrorActionPreference = 'Stop'
$total = 0
$evidence = @()
foreach ($target in @(
    @('windows-latest', 'Windows', 'net48'),
    @('ubuntu-latest', 'Linux', 'net8.0'),
    @('macos-latest', 'macOS', 'net8.0')
)) {
    $selection = & (Join-Path $PSScriptRoot 'get-ci-test-selection.ps1') -RunnerOs $target[1] -Framework $target[2]
    $root = Join-Path $Directory ('openscope-secondary-' + $target[0])
    $report = Join-Path $root 'results.trx'
    & (Join-Path $PSScriptRoot 'assert-ci-test-discovery.ps1') -Path (Join-Path $root 'selected-tests.txt') -ExpectedTests $selection.IsolatedTests -MaximumCases $selection.MaximumCases
    & (Join-Path $PSScriptRoot 'assert-ci-test-results.ps1') -Path $report -ExpectedTests $selection.IsolatedTests -MaximumCases $selection.MaximumCases
    [xml]$trx = Get-Content -LiteralPath $report -Raw
    $count = [int]$trx.TestRun.ResultSummary.Counters.executed
    $total += $count
    $evidence += [pscustomobject]@{ Platform = $target[1]; Framework = $target[2]; Executed = $count }
}
$reports = @(Get-ChildItem -LiteralPath $Directory -Recurse -File -Filter '*.trx')
if ($reports.Count -ne 3 -or $total -ne 20) {
    throw 'The complete secondary matrix must contain exactly three reports and 20 actual executions total.'
}
$evidence | ConvertTo-Json | Set-Content -LiteralPath (Join-Path $Directory 'secondary-matrix-summary.json') -Encoding utf8
Write-Host ('Verified shared secondary matrix budget: actual executions=' + $total + '/20.')
