using System;
using System.Collections.Generic;
using System.Linq;

namespace site.Infrastructure
{
    public static class CharacterExtensions
    {
        public static IEnumerable<char> ToCharArray(this IEnumerable<int> values)
        {
            return values.Select(c => Convert.ToChar(c));
        }
    }
}