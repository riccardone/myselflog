using System;

namespace MySelf.Diab.Model
{
    public class GlucoseLevel
    {
        public int Id { get; set; }
        
        public string Message { get; set; }
        
        public int Value { get; set; }
        
        public DateTime LogDate { get; set; }

        public LogProfile LogProfile { get; set; }

        public Guid GlobalId { get; set; }
    }
}
