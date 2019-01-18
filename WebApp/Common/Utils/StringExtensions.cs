using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        /// <summary>
        /// Parse string like "50.5%" into decimal value 0.505
        /// </summary>
        public static decimal? PercentToDecimal(this string stringPercent)
        {
            if (string.IsNullOrWhiteSpace(stringPercent)) return null;

            var trimmed = stringPercent.Replace("%", "").Trim();
            var result = decimal.TryParse(trimmed, out var decimalPercent);

            if (!result) return null;

            if (decimalPercent >= 100) return 1;
            if (decimalPercent <= 0) return null;

            return decimal.Round(decimalPercent / 100, 2);
        }

        /// <summary>
        /// Determines whenever value like "50.5%" can be converted into decimal value 0.505
        /// </summary>
        public static bool IsValidPercent(this string stringPercent)
        {
            if (string.IsNullOrWhiteSpace(stringPercent)) return true;

            var trimmed = stringPercent.Replace("%", "").Trim();
            var result = decimal.TryParse(trimmed, out var decimalPercent);

            return result;
        }
    }
}
