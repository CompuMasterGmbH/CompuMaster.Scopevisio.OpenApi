param(
    [Parameter(Mandatory = $true)][string]$Path,
    [string[]]$ExpectedTests = @(),
    [string[]]$ExpectedFixtures = @(),
    [ValidateRange(0, 20)][int]$MaximumCases = 0
)

$ErrorActionPreference = 'Stop'
if (!(Test-Path -LiteralPath $Path -PathType Leaf)) { throw 'Expected CI test results are missing.' }
[xml]$report = Get-Content -LiteralPath $Path -Raw
$counters = $report.TestRun.ResultSummary.Counters
$results = @($report.TestRun.Results.UnitTestResult)
if ($MaximumCases -gt 0 -and (
    [int]$counters.executed -ne [int]$counters.total -or
    [int]$counters.total -gt $MaximumCases -or $results.Count -gt $MaximumCases -or
    [int]$counters.total -ne $ExpectedTests.Count -or $results.Count -ne $ExpectedTests.Count -or
    @($results | Where-Object outcome -ne 'Passed').Count -gt 0 -or
    @($results | Select-Object -ExpandProperty testId -Unique).Count -ne $results.Count)) {
    throw 'Actual sample results must match the named cases within the shared 20-case budget.'
}
if (!$counters -or [int]$counters.executed -le 0 -or [int]$counters.passed -le 0 -or [int]$counters.failed -ne 0) {
    throw 'CI selection must execute and pass tests; an empty or failed report is not acceptance.'
}
if (($ExpectedTests.Count -gt 0 -or $ExpectedFixtures.Count -gt 0) -and [int]$counters.total -ne [int]$counters.passed) {
    throw 'Every selected sample must pass; partially skipped samples are not acceptance.'
}
$passedIds = @($report.TestRun.Results.UnitTestResult | Where-Object outcome -eq 'Passed' | ForEach-Object testId)
$methods = @($report.TestRun.TestDefinitions.UnitTest | Where-Object { $passedIds -contains $_.id } | ForEach-Object TestMethod)
$tests = @($methods | ForEach-Object { ($_.className -split ',')[0] + '.' + $_.name })
$fixtures = @($methods | ForEach-Object { ($_.className -split ',')[0] })
foreach ($expected in $ExpectedTests) {
    if (@($tests | Where-Object { $_ -eq $expected }).Count -ne 1) { throw 'A required named sample must pass exactly once.' }
}
foreach ($expected in $ExpectedFixtures) {
    if ($fixtures -notcontains $expected) { throw 'A required isolated sample fixture did not pass.' }
}
Write-Host ('Verified executed test results: passed=' + $counters.passed + '; failed=' + $counters.failed + '.')
