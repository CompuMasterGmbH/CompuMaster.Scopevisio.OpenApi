using System.Collections.Generic;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies optional query choices for the general journal reporting data source.</summary>
    /// <remarks>Null values omit query parameters. Explicit booleans and empty strings are sent unchanged; the server validates names, formatting choices, visibility and permissions. No client defaults are imposed.</remarks>
    public sealed class JournalDataSourceOptions
    {
        /// <summary>Gets or sets the optional report layout name, sent unchanged.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string ReportName { get; set; }

        /// <summary>Gets or sets the optional choice to include unassigned entries.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Unassigned { get; set; }

        /// <summary>Gets or sets the optional choice to force personal entries.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? ForcePersonalEntries { get; set; }

        /// <summary>Gets or sets the optional choice to hide cancellation entries.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? HideCancellationEntries { get; set; }

        /// <summary>Gets or sets the optional choice to consider the cost center dimension.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension1 { get; set; }

        /// <summary>Gets or sets the optional choice to consider the cost bearer dimension.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension2 { get; set; }

        /// <summary>Gets or sets the optional choice to consider the project dimension.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension3 { get; set; }

        /// <summary>Gets or sets the optional choice to consider dimension 4.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension4 { get; set; }

        /// <summary>Gets or sets the optional choice to consider dimension 5.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension5 { get; set; }

        /// <summary>Gets or sets the optional choice to consider dimension 6.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension6 { get; set; }

        /// <summary>Gets or sets the optional choice to consider dimension 7.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension7 { get; set; }

        /// <summary>Gets or sets the optional choice to consider dimension 8.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension8 { get; set; }

        /// <summary>Gets or sets the optional choice to consider dimension 9.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension9 { get; set; }

        /// <summary>Gets or sets the optional choice to consider dimension 10.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? Dimension10 { get; set; }

        /// <summary>Gets or sets the optional accounting entity numbers as CSV text, including leading zeros.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string AccountingEntityNumbers { get; set; }

        /// <summary>Gets or sets the optional selected fields as CSV text.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string Fields { get; set; }

        internal List<KeyValuePair<string, string>> ToQueryParameters()
        {
            var query = new List<KeyValuePair<string, string>>();
            if (ReportName != null) query.Add(new KeyValuePair<string, string>("reportName", ReportName));
            if (Unassigned.HasValue) query.Add(new KeyValuePair<string, string>("unassigned", Unassigned.Value ? "true" : "false"));
            if (ForcePersonalEntries.HasValue) query.Add(new KeyValuePair<string, string>("forcePersonalEntries", ForcePersonalEntries.Value ? "true" : "false"));
            if (HideCancellationEntries.HasValue) query.Add(new KeyValuePair<string, string>("hideCancellationEntries", HideCancellationEntries.Value ? "true" : "false"));
            if (Dimension1.HasValue) query.Add(new KeyValuePair<string, string>("dimension1", Dimension1.Value ? "true" : "false"));
            if (Dimension2.HasValue) query.Add(new KeyValuePair<string, string>("dimension2", Dimension2.Value ? "true" : "false"));
            if (Dimension3.HasValue) query.Add(new KeyValuePair<string, string>("dimension3", Dimension3.Value ? "true" : "false"));
            if (Dimension4.HasValue) query.Add(new KeyValuePair<string, string>("dimension4", Dimension4.Value ? "true" : "false"));
            if (Dimension5.HasValue) query.Add(new KeyValuePair<string, string>("dimension5", Dimension5.Value ? "true" : "false"));
            if (Dimension6.HasValue) query.Add(new KeyValuePair<string, string>("dimension6", Dimension6.Value ? "true" : "false"));
            if (Dimension7.HasValue) query.Add(new KeyValuePair<string, string>("dimension7", Dimension7.Value ? "true" : "false"));
            if (Dimension8.HasValue) query.Add(new KeyValuePair<string, string>("dimension8", Dimension8.Value ? "true" : "false"));
            if (Dimension9.HasValue) query.Add(new KeyValuePair<string, string>("dimension9", Dimension9.Value ? "true" : "false"));
            if (Dimension10.HasValue) query.Add(new KeyValuePair<string, string>("dimension10", Dimension10.Value ? "true" : "false"));
            if (AccountingEntityNumbers != null) query.Add(new KeyValuePair<string, string>("accountingEntityNumbers", AccountingEntityNumbers));
            if (Fields != null) query.Add(new KeyValuePair<string, string>("fields", Fields));
            return query;
        }
    }
}
