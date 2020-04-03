using System.Collections.Generic;

namespace site.Models
{
    public class PuzzleSet
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Puzzle> Puzzles { get; set; }
    }
}