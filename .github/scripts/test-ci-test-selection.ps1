$ErrorActionPreference = 'Stop'
foreach ($script in Get-ChildItem -LiteralPath $PSScriptRoot -Filter '*.ps1') {
    $tokens = $null
    $errors = $null
    $null = [Management.Automation.Language.Parser]::ParseFile($script.FullName, [ref]$tokens, [ref]$errors)
    if ($errors.Count) { throw 'A CI selection script has invalid PowerShell syntax.' }
}
$select = Join-Path $PSScriptRoot 'get-ci-test-selection.ps1'
$assert = Join-Path $PSScriptRoot 'assert-ci-test-results.ps1'
$full = & $select -RunnerOs Windows -Framework net8.0
if ($full.Coverage -ne 'full' -or $full.LegacyFilter -or $full.IsolatedFilter) { throw 'Windows/net8 must retain both complete suites.' }
foreach ($case in @(@('Linux', 'net8.0'), @('macOS', 'net8.0'), @('Windows', 'net48'))) {
    $sample = & $select -RunnerOs $case[0] -Framework $case[1]
    if ($sample.Coverage -ne 'sample' -or $sample.LegacyTests.Count -ne 3 -or $sample.IsolatedFixtures.Count -ne 5 -or !$sample.LegacyFilter -or !$sample.IsolatedFilter) {
        throw 'Other platform/runtime selections must retain bounded named samples.'
    }
}
$rejected = $false
try { $null = & $select -RunnerOs Linux -Framework net48 } catch { $rejected = $true }
if (!$rejected) { throw 'Unsupported runtime/platform combination accepted.' }
$temporary = Join-Path ([IO.Path]::GetTempPath()) ('openscope-ci-selection-' + [guid]::NewGuid().ToString('N') + '.trx')
try {
    foreach ($state in @('empty', 'failed', 'skipped', 'partial-skip', 'missing-sample', 'passed')) {
        $executed = if ($state -in @('empty', 'skipped')) { 0 } else { 1 }
        $passed = if ($state -in @('partial-skip', 'missing-sample', 'passed')) { 1 } else { 0 }
        $failed = if ($state -eq 'failed') { 1 } else { 0 }
        $total = if ($state -eq 'partial-skip') { 2 } elseif ($state -eq 'empty') { 0 } else { 1 }
        $class = if ($state -eq 'missing-sample') { 'OtherFixture' } else { 'SampleFixture' }
        $xml = "<TestRun><ResultSummary><Counters total='$total' executed='$executed' passed='$passed' failed='$failed'/></ResultSummary><Results><UnitTestResult outcome='Passed' testId='one'/></Results><TestDefinitions><UnitTest id='one'><TestMethod className='$class' name='Read'/></UnitTest></TestDefinitions></TestRun>"
        [IO.File]::WriteAllText($temporary, $xml)
        $accepted = $true
        try { & $assert -Path $temporary -ExpectedTests @('SampleFixture.Read') -ExpectedFixtures @('SampleFixture') } catch { $accepted = $false }
        if ($accepted -ne ($state -eq 'passed')) { throw 'Empty, failed, skipped or missing sample evidence was accepted.' }
    }
} finally {
    if (Test-Path -LiteralPath $temporary) { Remove-Item -LiteralPath $temporary }
}
Write-Host 'Offline CI selector and result-admission checks passed; no credentials, profiles or server access.'
