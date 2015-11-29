using CrossCutting.DomainBase;
using System;

namespace MySelf.Diab.Domain.Events
{
    public class PersonUpdated : IDomainEvent
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string Email { get; private set; }

        public string Country { get; private set; }

        public string PostalCode { get; private set; }

        public PersonUpdated(string firstName, string lastName, string email, string country, string postalCode)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Country = country;
            PostalCode = postalCode;
        }
    }
}
