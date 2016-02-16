using System;
using System.Collections.Generic;

namespace MySelf.Diab.Model
{
    public class LogProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Guid GlobalId { get; set; }

        public Person Person { get; set; }
        public SecurityLink SecurityLink { get; set; }
        public List<Friend> Friends { get; set; }
        public List<GlucoseLevel> GlucoseLevels { get; set; }
        public List<Terapy> Terapies { get; set; }
        public List<Food> Foods { get; set; }
        public const string DefaultName = "Default";
    }
}
