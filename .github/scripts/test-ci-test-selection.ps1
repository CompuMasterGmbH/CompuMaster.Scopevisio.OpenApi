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
if ($full.Coverage -ne 'full' -or $full.LegacyFilter -or $full.IsolatedFilter -or !$full.IncludeHistorical -or $full.MaximumCases -ne 0) { throw 'Windows/net8 must retain both complete suites.' }
foreach ($case in @(@('Linux', 'net8.0'), @('macOS', 'net8.0'), @('Windows', 'net48'))) {
    $sample = & $select -RunnerOs $case[0] -Framework $case[1]
    if ($sample.Coverage -ne 'sample' -or $sample.IncludeHistorical -or $sample.LegacyTests.Count -ne 0 -or $sample.IsolatedTests.Count -ne 20 -or $sample.MaximumCases -ne 20 -or $sample.LegacyFilter -or !$sample.IsolatedFilter -or $sample.IsolatedFilter.Contains('~') -or @($sample.IsolatedTests | Select-Object -Unique).Count -ne 20) {
        throw 'Other platform/runtime selections must retain exactly 20 named isolated cases and no historical account tests.'
    }
}
$rejected = $false
try { $null = & $select -RunnerOs Linux -Framework net48 } catch { $rejected = $true }
if (!$rejected) { throw 'Unsupported runtime/platform combination accepted.' }
$temporary = Join-Path ([IO.Path]::GetTempPath()) ('openscope-ci-selection-' + [guid]::NewGuid().ToString('N') + '.trx')
try {
    $discoveryAssert = Join-Path $PSScriptRoot 'assert-ci-test-discovery.ps1'
    foreach ($state in @('empty', 'expanded', 'duplicate', 'missing', 'extra', 'overflow', 'passed')) {
        $names = switch ($state) {
            'empty' { @() }
            'expanded' { @('Read(1)', 'Write') }
            'duplicate' { @('Read', 'Read') }
            'missing' { @('Read') }
            'extra' { @('Read', 'Write', 'Other') }
            'overflow' { @(1..21 | ForEach-Object { 'Read' + $_ }) }
            'passed' { @('Read', 'Write') }
        }
        [IO.File]::WriteAllLines($temporary, @('Localized discovery heading:') + @($names | ForEach-Object { '    ' + $_ }))
        $expectedDiscovery = if ($state -eq 'overflow') { @($names | ForEach-Object { 'SampleFixture.' + $_ }) } else { @('SampleFixture.Read', 'SampleFixture.Write') }
        $accepted = $true
        try { & $discoveryAssert -Path $temporary -ExpectedTests $expectedDiscovery -MaximumCases 20 } catch { $accepted = $false }
        if ($accepted -ne ($state -eq 'passed')) { throw 'Uncertain or over-budget discovered cases were accepted.' }
    }
    foreach ($state in @('empty', 'failed', 'skipped', 'partial-skip', 'missing-sample', 'overflow', 'passed')) {
        $executed = if ($state -in @('empty', 'skipped')) { 0 } else { 1 }
        $passed = if ($state -in @('partial-skip', 'missing-sample', 'overflow', 'passed')) { 1 } else { 0 }
        $failed = if ($state -eq 'failed') { 1 } else { 0 }
        $total = if ($state -eq 'partial-skip') { 2 } elseif ($state -eq 'empty') { 0 } else { 1 }
        $class = if ($state -eq 'missing-sample') { 'OtherFixture' } else { 'SampleFixture' }
        $xml = "<TestRun><ResultSummary><Counters total='$total' executed='$executed' passed='$passed' failed='$failed'/></ResultSummary><Results><UnitTestResult outcome='Passed' testId='one'/></Results><TestDefinitions><UnitTest id='one'><TestMethod className='$class' name='Read'/></UnitTest></TestDefinitions></TestRun>"
        if ($state -eq 'overflow') {
            $xml = "<TestRun><ResultSummary><Counters total='21' executed='21' passed='21' failed='0'/></ResultSummary><Results>" +
                ((1..21 | ForEach-Object { "<UnitTestResult outcome='Passed' testId='$_'/>" }) -join '') +
                "</Results><TestDefinitions><UnitTest id='1'><TestMethod className='SampleFixture' name='Read'/></UnitTest></TestDefinitions></TestRun>"
        }
        [IO.File]::WriteAllText($temporary, $xml)
        $accepted = $true
        try { & $assert -Path $temporary -ExpectedTests @('SampleFixture.Read') -ExpectedFixtures @('SampleFixture') -MaximumCases 20 } catch { $accepted = $false }
        if ($accepted -ne ($state -eq 'passed')) { throw 'Empty, failed, skipped or missing sample evidence was accepted.' }
    }
    # Independent budget boundary: all names/counters are valid, only 21 exceeds
    # the limit. Also reject duplicate IDs and inconsistent execution counters.
    foreach ($state in @('boundary20', 'over21', 'duplicate-id', 'wrong-executed', 'wrong-passed', 'missing-result')) {
        $count = if ($state -eq 'over21') { 21 } else { 20 }
        $expected = @(1..$count | ForEach-Object { 'SampleFixture.Case' + $_ })
        [IO.File]::WriteAllLines($temporary, @('Tests:') + @(1..$count | ForEach-Object { '    Case' + $_ }))
        $accepted = $true
        try { & $discoveryAssert -Path $temporary -ExpectedTests $expected -MaximumCases 20 } catch { $accepted = $false }
        if ($accepted -ne ($count -le 20)) { throw 'Discovery budget boundary was not enforced.' }
        $executed = if ($state -eq 'wrong-executed') { 21 } else { $count }
        $passed = if ($state -eq 'wrong-passed') { 19 } else { $count }
        $resultCount = if ($state -eq 'missing-result') { 19 } else { $count }
        $results = (1..$resultCount | ForEach-Object {
            $id = if ($state -eq 'duplicate-id') { 1 } else { $_ }
            "<UnitTestResult outcome='Passed' testId='$id'/>"
        }) -join ''
        $definitions = (1..$count | ForEach-Object { "<UnitTest id='$_'><TestMethod className='SampleFixture' name='Case$_'/></UnitTest>" }) -join ''
        $xml = "<TestRun><ResultSummary><Counters total='$count' executed='$executed' passed='$passed' failed='0'/></ResultSummary><Results>$results</Results><TestDefinitions>$definitions</TestDefinitions></TestRun>"
        [IO.File]::WriteAllText($temporary, $xml)
        $accepted = $true
        try { & $assert -Path $temporary -ExpectedTests $expected -MaximumCases 20 } catch { $accepted = $false }
        if ($accepted -ne ($state -eq 'boundary20')) { throw 'Actual result budget/identity/counter boundary was not enforced.' }
    }
} finally {
    if (Test-Path -LiteralPath $temporary) { Remove-Item -LiteralPath $temporary }
}
Write-Host 'Offline CI selector and result-admission checks passed; no credentials, profiles or server access.'
