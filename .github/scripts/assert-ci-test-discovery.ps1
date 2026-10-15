param(
    [Parameter(Mandatory = $true)][string]$Path,
    [Parameter(Mandatory = $true)][string[]]$ExpectedTests,
    [ValidateRange(1, 20)][int]$MaximumCases = 20
)

$ErrorActionPreference = 'Stop'
if (!(Test-Path -LiteralPath $Path -PathType Leaf)) { throw 'Selected test discovery is missing.' }
# VSTest indents each discovered NUnit case by four spaces. Localized headings
# are irrelevant; unexpected indented diagnostics fail closed before execution.
$cases = @(Get-Content -LiteralPath $Path | ForEach-Object {
    if ($_ -match '^\s{4}(\S.*)$') { $Matches[1].TrimEnd() }
})
$expected = @($ExpectedTests | ForEach-Object { ($_ -split '\.')[-1] })
if (!$cases.Count -or $cases.Count -gt $MaximumCases -or $cases.Count -ne $expected.Count -or
    @($cases | Select-Object -Unique).Count -ne $cases.Count -or
    @($expected | Select-Object -Unique).Count -ne $expected.Count -or
    @(Compare-Object $expected $cases).Count) {
    throw 'Discovery must match the named nonparameterized cases within the shared 20-case budget.'
}
Write-Host ('Verified selected discovery before execution: cases=' + $cases.Count + '.')
