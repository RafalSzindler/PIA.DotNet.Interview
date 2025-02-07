using PIA.DotNet.Interview.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PIA.DotNet.Interview.Core.Logging
{
    public class CrudLogger : ILogger
    {
        private readonly List<LogEntry> _logEntries;

        public CrudLogger()
        {
            _logEntries = new List<LogEntry>();
        }

        private void Log(string operation, string entityName, string details)
        {
            var logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Operation = operation,
                EntityName = entityName,
                Details = details,
                Id = Guid.NewGuid()
            };

            _logEntries.Add(logEntry);
            Console.WriteLine($"Logged: {operation} - {entityName} - {details}");
        }

        public void LogCreate(string entityName, string details)
        {
            Log("CREATE", entityName, details);
        }

        public void LogRead(string entityName, string details)
        {
            Log("READ", entityName, details);
        }

        public void LogUpdate(string entityName, string details)
        {
            Log("UPDATE", entityName, details);
        }

        public void LogDelete(string entityName, string details)
        {
            Log("DELETE", entityName, details);
        }
    }
}