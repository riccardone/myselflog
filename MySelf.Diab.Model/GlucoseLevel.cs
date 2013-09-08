using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Diab.Model
{
    public class GlucoseLevel
    {
        public int Id { get; set; }
        //public int LogProfileId { get; set; }
        
        public string Message { get; set; }
        
        public int Value { get; set; }
        
        public DateTime LogDate { get; set; }

        public LogProfile LogProfile { get; set; }

        public Guid GlobalId { get; set; }
    }
}
