namespace CompuMaster.Scopevisio.OpenApi.Client
{
    public partial class Configuration
    {
        /// <summary>Gets or sets the optional numeric organisation identifier used for credential authorization.</summary>
        /// <value>The exact Scopevisio organisation identifier, or null to retain default or name-based authorization.</value>
        /// <remarks>
        /// A specified identifier takes precedence over <see cref="OrganisationName"/> and is not a Teamwork tenant identifier.
        /// High-level credential authorization validates the returned token and a fresh account context before installing the token.
        /// It performs no organisation lookup or fallback to a default organisation. Clearing the identifier restores existing name/default behavior.
        /// </remarks>
        public virtual long? OrganisationId { get; set; }
    }
}
