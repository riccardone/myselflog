using CrossCutting.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Diab.Domain.Events
{
    public class LogProfileCreated : IDomainEvent
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string PersonId { get; set; }
        public LogProfileCreated(string id, string name, string personId)
        {
            Id = id;
            Name = name;
            PersonId = personId;
        }
    }
}
