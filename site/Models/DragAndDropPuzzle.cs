using System.Collections.Generic;

namespace site.Models
{
    public class DragAndDropPuzzle : Puzzle
    {
        public IDictionary<string,string> Style { get; set; }
        public IEnumerable<Zone> Zones { get; set; }
        public IEnumerable<string> Dictionary { get; set; }
    }

    public class Zone
    {
        public string Name { get; set; }
        public IDictionary<string,string> Style { get; set; }
        public IEnumerable<string> Requires { get; set; }
    }

}