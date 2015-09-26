using CrossCutting.DomainBase;
using System;

namespace MySelf.Diab.Domain.Events
{
    public class TerapyAdded : IDomainEvent
    {
        public string Message { get; private set; }
        public int Value { get; private set; }
        public bool IsSlow { get; private set; }
        public DateTime LogDate { get; private set; }

        public TerapyAdded(string message, int value, bool isSlow, DateTime logDate)
        {
            Message = message;
            Value = value;
            IsSlow = isSlow;
            LogDate = logDate;
        }
    }
}
