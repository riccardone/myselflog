using CrossCutting.DomainBase;
using MySelf.Diab.Domain.Events;
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
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Country { get; private set; }
        public string PostalCode { get; private set; }
        public DateTime? DateOfBirth { get; private set; }

        public override string AggregateId
        {
            get
            {
                return Id;
            }
        }

        public Person()
        {
            RegisterTransition<PersonCreated>(Apply);
        }

        private void Apply(PersonCreated obj)
        {
            throw new NotImplementedException();
        }
    }
}
