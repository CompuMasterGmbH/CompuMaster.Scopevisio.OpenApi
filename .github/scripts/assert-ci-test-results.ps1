param(
    [Parameter(Mandatory = $true)][string]$Path,
    [string[]]$ExpectedTests = @(),
    [string[]]$ExpectedFixtures = @()
)

$ErrorActionPreference = 'Stop'
if (!(Test-Path -LiteralPath $Path -PathType Leaf)) { throw 'Expected CI test results are missing.' }
[xml]$report = Get-Content -LiteralPath $Path -Raw
$counters = $report.TestRun.ResultSummary.Counters
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
    if ($tests -notcontains $expected) { throw 'A required named historical sample did not pass.' }
}
foreach ($expected in $ExpectedFixtures) {
    if ($fixtures -notcontains $expected) { throw 'A required isolated sample fixture did not pass.' }
}
Write-Host ('Verified executed test results: passed=' + $counters.passed + '; failed=' + $counters.failed + '.')
