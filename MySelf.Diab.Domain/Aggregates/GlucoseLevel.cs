using System;

namespace MySelf.Diab.Domain.Aggregates
{
    public class GlucoseLevel
    {
        public string Message { get; set; }
        
        public int Value { get; set; }
        
        public DateTime LogDate { get; set; }
    }
}
