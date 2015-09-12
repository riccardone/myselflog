using CrossCutting.DomainBase;
using System;

namespace MySelf.Diab.Domain.Events
{
    public class GlucoseLevelAdded : IDomainEvent
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
