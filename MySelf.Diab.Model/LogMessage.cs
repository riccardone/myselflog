using System;
using System.Collections.Generic;

namespace MySelf.Diab.Model
{
    public class LogMessage
    {
        public Guid? GlobalId { get; set; }
        public string Message { get; set; }
        public int Value { get; set; }
        public DateTime LogDate { get; set; }
        public string Email { get; set; }
        public Guid ProfileId { get; set; }
        public int TerapyValue { get; set; }
        public bool IsSlow { get; set; }
        public Guid? TerapyGlobalId { get; set; }
        public int Calories { get; set; }
        public string FoodTypes { get; set; }
    }
}
