using System;
using System.Collections.Generic;
using System.Text;

namespace coding_tracker
{
    internal class CodingSession
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
