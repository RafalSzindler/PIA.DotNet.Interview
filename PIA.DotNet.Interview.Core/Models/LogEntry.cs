using System;
using System.Collections.Generic;
using System.Text;

namespace PIA.DotNet.Interview.Core.Models
{
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Operation { get; set; }
        public string EntityName { get; set; }
        public string Details { get; set; }
        public Guid Id { get; set; }

    }
}
