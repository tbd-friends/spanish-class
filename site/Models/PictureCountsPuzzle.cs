using System.Collections.Generic;

namespace site.Models
{
    public class PictureCountsPuzzle : Puzzle
    {
        public IEnumerable<Answer> Options { get; set; }
        public IEnumerable<string> Key { get; set; }
        public string Text { get; set; }
        public Dictionary<string,string> Style { get; set; }
    }
}