param(
    [Parameter(Mandatory = $true)][ValidateSet('Windows', 'Linux', 'macOS')][string]$RunnerOs,
    [Parameter(Mandatory = $true)][ValidateSet('net8.0', 'net48')][string]$Framework
)

$ErrorActionPreference = 'Stop'
if ($Framework -eq 'net48' -and $RunnerOs -ne 'Windows') {
    throw '.NET Framework samples require Windows.'
}
$full = $RunnerOs -eq 'Windows' -and $Framework -eq 'net8.0'
$legacyTests = @(
    'CompuMaster.Scopevisio.OpenApi.Test.AdditionalApiTests.GetApplicationContextTest',
    'CompuMaster.Scopevisio.OpenApi.Test.AdditionalApiTests.InstanceTest',
    'CompuMaster.Scopevisio.OpenApi.Test.AuthorizationApiTests.InstanceTest'
)
$isolatedFixtures = @(
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.AuthorizationCancellationTests',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.BasicDataSourceProtocolTests',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.JournalFileDownloadTests',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.ApiExceptionCauseTests',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.TestConfigBootstrapTests',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.OrganisationAuthorizationTests'
)
[pscustomobject]@{
    Coverage = if ($full) { 'full' } else { 'sample' }
    LegacyFilter = if ($full) { '' } else { ($legacyTests | ForEach-Object { 'FullyQualifiedName=' + $_ }) -join '|' }
    IsolatedFilter = if ($full) { '' } else { ($isolatedFixtures | ForEach-Object { 'FullyQualifiedName~' + $_ + '.' }) -join '|' }
    LegacyTests = if ($full) { @() } else { $legacyTests }
    IsolatedFixtures = if ($full) { @() } else { $isolatedFixtures }
}
