using System;

namespace MySelf.Diab.Domain.Aggregates.ValueObjects
{
    public class Terapy
    {
        public string Message { get; set; }

        public int Value { get; set; }

        public bool IsSlow { get; set; }

        public DateTime LogDate { get; set; }
    }
}
