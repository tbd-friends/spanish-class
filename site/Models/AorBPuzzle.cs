using System.Collections.Generic;

namespace site.Models
{
    public class AorBPuzzle : Puzzle
    {
        public IEnumerable<Question> Questions { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
    }
}