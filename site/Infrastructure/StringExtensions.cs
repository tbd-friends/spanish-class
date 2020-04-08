using System;
using System.Collections.Generic;
using System.Linq;

namespace site.Infrastructure
{
    public static class StringExtensions
    {
        public static IEnumerable<char> ToCharArray(this IEnumerable<int> values)
        {
            return values.Select(c => Convert.ToChar(c));
        }

        public static IEnumerable<string> Randomize(this IEnumerable<string> values)
        {
            return values.Select(s => (id: Guid.NewGuid(), value: s)).OrderBy(o => o.id).Select(s => s.value);
        }
    }
}