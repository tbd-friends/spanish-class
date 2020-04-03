using System;

namespace site.Infrastructure
{
    public class GameCompleteStatus
    {
        public string IpAddress { get; set; }
        public string EntryCode { get; set; }
        public string Name { get; set; }
        public TimeSpan Completed { get; set; }
    }
}