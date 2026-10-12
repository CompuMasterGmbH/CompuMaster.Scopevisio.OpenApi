using System;
using System.Collections.Generic;
using System.Globalization;

namespace CompuMaster.Scopevisio.OpenApi.Client
{
    internal static class PositionReadQuery
    {
        internal static List<KeyValuePair<string, string>> Create(string startDate, string endDate, string fields, int? page, int? pageSize)
        {
            ValidateDate(startDate, nameof(startDate));
            ValidateDate(endDate, nameof(endDate));
            if (page.HasValue && page.Value < 0) throw new ArgumentOutOfRangeException(nameof(page), "The zero-based page must not be negative.");
            if (pageSize.HasValue && (pageSize.Value < 1 || pageSize.Value > 5000)) throw new ArgumentOutOfRangeException(nameof(pageSize), "The page size must be between 1 and 5000.");
            var query = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("startDate", startDate),
                new KeyValuePair<string, string>("endDate", endDate)
            };
            if (fields != null) query.Add(new KeyValuePair<string, string>("fields", fields));
            if (page.HasValue) query.Add(new KeyValuePair<string, string>("page", page.Value.ToString(CultureInfo.InvariantCulture)));
            if (pageSize.HasValue) query.Add(new KeyValuePair<string, string>("pageSize", pageSize.Value.ToString(CultureInfo.InvariantCulture)));
            return query;
        }

        private static void ValidateDate(string value, string parameterName)
        {
            if (value == null || !DateTime.TryParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
                throw new ArgumentException("The required date must be valid in dd.MM.yyyy format.", parameterName);
        }
    }
}
