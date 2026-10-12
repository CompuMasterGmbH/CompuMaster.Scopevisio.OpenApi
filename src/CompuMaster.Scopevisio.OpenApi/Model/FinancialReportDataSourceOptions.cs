using System.Collections.Generic;

namespace CompuMaster.Scopevisio.OpenApi.Model
{
    /// <summary>Specifies optional query choices for the financial reporting data source.</summary>
    /// <remarks>Null values omit query parameters. Explicit booleans and empty strings are sent unchanged; the server validates names, formatting choices, visibility and permissions. No client defaults are imposed.</remarks>
    public sealed class FinancialReportDataSourceOptions
    {
        /// <summary>Gets or sets the optional budget group or plan name, sent unchanged.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string BudgetGroupName { get; set; }

        /// <summary>Gets or sets the optional organisation names as CSV text; null retains the documented current-organisation server default.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string Organisations { get; set; }

        /// <summary>Gets or sets the optional choice to show accounts.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? ShowAccounts { get; set; }

        /// <summary>Gets or sets the optional choice to show GUV accounts.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? ShowGuvAccounts { get; set; }

        /// <summary>Gets or sets the optional choice to show empty rows.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? ShowEmptyRows { get; set; }

        /// <summary>Gets or sets the optional choice to show line numbers.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? ShowLineNumbers { get; set; }

        /// <summary>Gets or sets the optional choice to use a foreign language.</summary>
        /// <value>Null omits the parameter; false and true are sent explicitly.</value>
        public bool? UseForeignLanguage { get; set; }

        /// <summary>Gets or sets the optional monetary suffix; documented choices are NoSuffix and DebitCredit, with server default DebitCredit.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string AmountSuffix { get; set; }

        /// <summary>Gets or sets the optional monetary format; documented choices are NoDecimalDigits, TwoDecimalDigits and DivideByThousand, with server default TwoDecimalDigits.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string AmountFormatting { get; set; }

        /// <summary>Gets or sets the optional percentage format; documented choices are NoDecimalDigits, OneDecimalDigit and TwoDecimalDigits, with server default TwoDecimalDigits.</summary>
        /// <value>Null omits the parameter; an empty string is sent for server validation.</value>
        public string PercentFormatting { get; set; }

        internal List<KeyValuePair<string, string>> ToQueryParameters()
        {
            var query = new List<KeyValuePair<string, string>>();
            if (BudgetGroupName != null) query.Add(new KeyValuePair<string, string>("budgetGroupName", BudgetGroupName));
            if (Organisations != null) query.Add(new KeyValuePair<string, string>("organisations", Organisations));
            if (ShowAccounts.HasValue) query.Add(new KeyValuePair<string, string>("showAccounts", ShowAccounts.Value ? "true" : "false"));
            if (ShowGuvAccounts.HasValue) query.Add(new KeyValuePair<string, string>("showGuvAccounts", ShowGuvAccounts.Value ? "true" : "false"));
            if (ShowEmptyRows.HasValue) query.Add(new KeyValuePair<string, string>("showEmptyRows", ShowEmptyRows.Value ? "true" : "false"));
            if (ShowLineNumbers.HasValue) query.Add(new KeyValuePair<string, string>("showLineNumbers", ShowLineNumbers.Value ? "true" : "false"));
            if (UseForeignLanguage.HasValue) query.Add(new KeyValuePair<string, string>("useForeignLanguage", UseForeignLanguage.Value ? "true" : "false"));
            if (AmountSuffix != null) query.Add(new KeyValuePair<string, string>("amountSuffix", AmountSuffix));
            if (AmountFormatting != null) query.Add(new KeyValuePair<string, string>("amountFormatting", AmountFormatting));
            if (PercentFormatting != null) query.Add(new KeyValuePair<string, string>("percentFormatting", PercentFormatting));
            return query;
        }
    }
}
