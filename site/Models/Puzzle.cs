using System;

namespace site.Models
{
    public class Puzzle
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}