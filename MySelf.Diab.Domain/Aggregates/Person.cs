using CrossCutting.DomainBase;
using MySelf.Diab.Domain.Events;
using System;

namespace MySelf.Diab.Domain.Aggregates
{
    public class Person : AggregateBase
    {
        public Guid Id { get; set; }
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
                return Id.ToString();
            }
        }

        public Person(Guid id, string firstName, string lastName, string email, string country, string postalCode, DateTime? dateOfBirth)
            : this()
        {
            // TODO validate
            RaiseEvent(new PersonCreated(id, firstName, lastName, email, country, postalCode, dateOfBirth));
        }

        public Person()
        {
            RegisterTransition<PersonCreated>(Apply);
        }

        private void Apply(PersonCreated obj)
        {
            Id = obj.Id;
            FirstName = obj.FirstName;
            LastName = obj.LastName;
            Email = obj.Email;
            Country = obj.Country;
            PostalCode = obj.PostalCode;
            DateOfBirth = obj.DateOfBirth;
        }

        public static Person CreatePerson(Guid id, string firstName, string lastName, string email, string country, string postalCode, DateTime? dateOfBirth)
        {
            return new Person(id, firstName, lastName, email, country, postalCode, dateOfBirth);
        }

        public void UpdatePerson(string firstName, string lastName, string email, string country, string postalCode)
        {
            if (!FirstName.Equals(firstName) || !LastName.Equals(lastName) || !Email.Equals(email) ||
                !Country.Equals(country) || !PostalCode.Equals(postalCode))
            {
                RaiseEvent(new PersonUpdated(firstName, lastName, email, country, postalCode));
            }
        }
    }
}
