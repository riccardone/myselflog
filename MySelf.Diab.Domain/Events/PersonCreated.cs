using CrossCutting.DomainBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Diab.Domain.Events
{
    public class PersonCreated : IDomainEvent
    {
        public string Id { get; private set; }

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Country { get; private set; }

        public string PostalCode { get; private set; }

        public DateTime? DateOfBirth { get; private set; }

        public PersonCreated(string id, string firstName, string lastName, string email, string country, string postalCode, DateTime? dateOfBirth)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Country = country;
            PostalCode = postalCode;
            DateOfBirth = dateOfBirth;
        }
    }
}
