using System;
using System.Collections.Generic;
using System.Text;

namespace PIA.DotNet.Interview.Core.Logging
{
    public interface ILogger
    {
        void LogCreate(string entityName, string details);
        void LogRead(string entityName, string details);
        void LogUpdate(string entityName, string details);
        void LogDelete(string entityName, string details);
    }
}
