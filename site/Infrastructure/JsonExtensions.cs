using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace site.Infrastructure
{
    public static class JsonExtensions
    {
        public static IEnumerable<string> AsEnumerable(this JArray @object)
        {
            return (@object).Select(s => (string)s);
        }
    }
}