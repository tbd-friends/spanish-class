using System.Collections.Generic;

namespace site.Models
{
    public class Question
    {
        public string Text { get; set; }
        public IEnumerable<string> Options { get; set; }
    }
}