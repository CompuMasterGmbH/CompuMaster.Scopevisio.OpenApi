namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Identifies the kind of project revenue returned by the server.</summary>
    public enum ProjectRevenueType
    {
        /// <summary>Represents planned revenue.</summary>
        Planning = 0,
        /// <summary>Represents realized revenue.</summary>
        Realization = 1,
        /// <summary>Represents the difference between planned and realized revenue.</summary>
        Deviation = 2
    }
}
