$ErrorActionPreference = 'Stop'
$temporary = Join-Path ([IO.Path]::GetTempPath()) ('openscope-matrix-policy-' + [guid]::NewGuid().ToString('N'))
try {
    foreach ($state in @('passed', 'extra-report', 'missing-report', 'overflow')) {
        $root = Join-Path $temporary $state
        foreach ($target in @(@('windows-latest', 'Windows', 'net48'), @('ubuntu-latest', 'Linux', 'net8.0'), @('macos-latest', 'macOS', 'net8.0'))) {
            $selection = & (Join-Path $PSScriptRoot 'get-ci-test-selection.ps1') -RunnerOs $target[1] -Framework $target[2]
            $directory = Join-Path $root ('openscope-secondary-' + $target[0])
            $null = New-Item -ItemType Directory -Path $directory -Force
            $expected = $selection.IsolatedTests
            if ($state -eq 'overflow' -and $target[1] -eq 'Linux') { $expected += 'OtherFixture.Extra' }
            $cases = @($expected | ForEach-Object { '    ' + ($_ -split '[.]')[-1] })
            [IO.File]::WriteAllLines((Join-Path $directory 'selected-tests.txt'), @('Tests:') + $cases)
            $results = @()
            $definitions = @()
            for ($index = 0; $index -lt $expected.Count; $index++) {
                $name = $expected[$index]
                $dot = $name.LastIndexOf('.')
                $class = $name.Substring(0, $dot)
                $method = $name.Substring($dot + 1)
                $results += "<UnitTestResult outcome='Passed' testId='$index'/>"
                $definitions += "<UnitTest id='$index'><TestMethod className='$class' name='$method'/></UnitTest>"
            }
            $count = $expected.Count
            $xml = "<TestRun><ResultSummary><Counters total='$count' executed='$count' passed='$count' failed='0'/></ResultSummary><Results>" + ($results -join '') + '</Results><TestDefinitions>' + ($definitions -join '') + '</TestDefinitions></TestRun>'
            if ($state -ne 'missing-report' -or $target[1] -ne 'macOS') { [IO.File]::WriteAllText((Join-Path $directory 'results.trx'), $xml) }
        }
        if ($state -eq 'extra-report') { [IO.File]::WriteAllText((Join-Path $root 'unexpected.trx'), '<TestRun/>') }
        $accepted = $true
        try { & (Join-Path $PSScriptRoot 'assert-ci-sample-matrix.ps1') -Directory $root } catch { $accepted = $false }
        if ($accepted -ne ($state -eq 'passed')) { throw 'Shared secondary matrix admission accepted missing, extra or over-budget results.' }
    }
} finally {
    # Only this newly generated, task-owned temporary directory is removed.
    if (Test-Path -LiteralPath $temporary) {
        $resolved = [IO.Path]::GetFullPath($temporary)
        $parent = [IO.Path]::GetFullPath([IO.Path]::GetTempPath()).TrimEnd([IO.Path]::DirectorySeparatorChar) + [IO.Path]::DirectorySeparatorChar
        if (!$resolved.StartsWith($parent, [StringComparison]::OrdinalIgnoreCase) -or [IO.Path]::GetFileName($resolved) -notlike 'openscope-matrix-policy-*') { throw 'Unexpected temporary cleanup scope.' }
        Remove-Item -LiteralPath $resolved -Recurse -Force
    }
}
Write-Host 'Credential-free combined secondary matrix policy controls passed.'
