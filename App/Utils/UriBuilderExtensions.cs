using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace App.Utils
{
    public static class UriBuilderExtensions
    {
        public static void AddQueryParameters<TValue>(this UriBuilder uriBuilder, List<KeyValuePair<string, TValue>> queryParams)
        {
            Assert.IsNotNull(queryParams, nameof(queryParams));
            uriBuilder.Query += queryParams.Aggregate(string.Empty, (s, pair) => s + $"&{pair.Key}={pair.Value.ToString()}");
        }

        public static IDictionary<string, string> ParsedQuery(this UriBuilder uriBuilder)
        {
            var matches = Regex.Matches(uriBuilder.Query, @"[\?&](?<name>[^&=]+)=(?<value>[^&=]+)");
            var dict = new Dictionary<string, string>();
            foreach (Match match in matches)
            {
                dict[match.Groups["name"].Value] = match.Groups["value"].Value;
            }
            return dict;
        }
    }
}
