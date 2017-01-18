using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

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
            return uriBuilder.Query
                .Split(new[] {"&"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(param =>
                {
                    var paramValue = param.Split(new[] {"="}, StringSplitOptions.None);
                    return new KeyValuePair<string, string>(paramValue[0], paramValue[1]);
                }).ToImmutableDictionary();
        }
    }
}
