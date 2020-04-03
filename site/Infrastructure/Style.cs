using System.Collections.Generic;
using System.Linq;

namespace site.Infrastructure
{
    public static class Style
    {
        public static string FromDictionary(IDictionary<string, string> style)
        {
            return string.Join(";", style.Select(s => $"{s.Key}:{s.Value}").ToArray());
        }
    }
}