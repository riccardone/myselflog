using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Diab.Domain.Events
{
    public class GlucoseLevelAdded
    {
        public string Message { get; private set; }

        public int Value { get; private set; }

        public DateTime LogDate { get; private set; }

        public GlucoseLevelAdded(string message, int value, DateTime logDate)
        {
            Message = message;
            Value = value;
            LogDate = logDate;
        }
    }
}
