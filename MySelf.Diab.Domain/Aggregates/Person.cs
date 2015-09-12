using CrossCutting.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Diab.Domain.Aggregates
{
    public class Person : AggregateBase
    {
        public string Id { get; set; }
        public override string AggregateId
        {
            get
            {
                return Id;
            }
        }
    }
}
