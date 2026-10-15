param(
    [Parameter(Mandatory = $true)][ValidateSet('Windows', 'Linux', 'macOS')][string]$RunnerOs,
    [Parameter(Mandatory = $true)][ValidateSet('net8.0', 'net48')][string]$Framework
)

$ErrorActionPreference = 'Stop'
if ($Framework -eq 'net48' -and $RunnerOs -ne 'Windows') {
    throw '.NET Framework samples require Windows.'
}
$full = $RunnerOs -eq 'Windows' -and $Framework -eq 'net8.0'
# Exact nonparameterized methods share one budget across all projects.
$isolatedTests = @(
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.AuthorizationCancellationTests.AuthorizationInstallsTokensAndRefreshUsesTheInstalledRefreshToken',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.OrganisationAuthorizationTests.PreCancellationDoesNotSendHttpOrInstallState',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.OrganisationAuthorizationTests.RefreshValidatesFreshIdentityAndKeepsTheOriginalRefreshWireContract',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.OrganisationAuthorizationTests.RefreshRejectsADifferentConfiguredOrganisationBeforeHttp',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.TestConfigBootstrapTests.ColdCacheFirstAccountReadUsesTheFreshlyObtainedToken',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.TestConfigBootstrapTests.FailedAuthorizationPreservesTheOriginalFailureAndDoesNotCacheAToken',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.ContactSearchProtocolTests.CancellationReachesTheActiveContactRequestWithoutChangingAuthorization',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.ContactDetailReadTests.ExplicitKeySelectorIsEscapedWithoutNormalization',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.AccountingReadProtocolTests.IntegerAndBooleanQueriesUseInvariantWireValues',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.AccountingReadProtocolTests.MappingRecordsPreserveLeadingZerosInt64EpochsAndCopiedEntries',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.AccountingReportDownloadTests.ExportChoicesAreCopiedBeforeActiveHttpCompletes',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.AccountMappingWriteTests.EntryArraysAreDefensiveAndTypedNullRemovesFields',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.MaterialMutationTests.CandidateResultsAndFeedbackRemainCompleteWithoutAggregateSuccess',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.MaterialMutationTests.NestedVendorProductUnitAndPositionArraysAreDefensiveCopies',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.IncomingInvoiceImportTests.ByteFactoryRequiresArrayWithoutChoosingFilename',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.PostingImportTests.ActiveCancellationStopsNativeHttpWithoutReplay',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.DatevTransferTests.ImportByteFactoryCopiesAllBytesWithoutFileOrTextConversion',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.ProductWriteTests.GroupNativeCancellationAndSnapshotHaveNoReplay',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.AccountUserMetadataTests.CurrentAccountFieldAuditUsesActualDeclaredTypesAndKinds',
    'CompuMaster.Scopevisio.OpenApi.AsyncTests.TokenCustomerMetadataTests.CurrentPrimaryFieldsAreOptionalStringsAndNamespacesAreDistinct'
)
[pscustomobject]@{
    Coverage = if ($full) { 'full' } else { 'sample' }
    IncludeHistorical = $full
    LegacyFilter = ''
    LegacyTests = @()
    IsolatedFilter = if ($full) { '' } else { ($isolatedTests | ForEach-Object { 'FullyQualifiedName=' + $_ }) -join '|' }
    IsolatedTests = if ($full) { @() } else { $isolatedTests }
    MaximumCases = if ($full) { 0 } else { 20 }
}
