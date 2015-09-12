using CrossCutting.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Diab.Domain.Aggregates
{
    public class LogProfile : AggregateBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PersonId { get; set; }
        public SecurityLink SecurityLink { get; set; }
        public List<string> FriendsIds { get; set; }
        public List<GlucoseLevel> GlucoseLevels { get; set; }
        public List<Terapy> Terapies { get; set; }

        public const string DefaultName = "Default";

        public override string AggregateId
        {
            get
            {
                return Id;
            }
        }
    }
}
