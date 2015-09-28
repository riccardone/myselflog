using System;
using CrossCutting.Repository;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;

namespace MySelf.Diab.EventStore
{
    public class EventStorePersonCommands : IPersonCommands 
    {
        private readonly IDomainRepository _repository;

        public EventStorePersonCommands(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void Create(Person item)
        {
            var person = Domain.Aggregates.Person.CreatePerson(Guid.NewGuid(), item.FirstName, item.LastName, item.Email,
                item.Country, item.PostalCode, item.DateOfBirth);
            _repository.Save(person);
        }

        public void Update(Person item)
        {
            throw new NotImplementedException();
            //var currentUser = _db.People.Include(e => e.LogProfiles).FirstOrDefault(p => p.Email == item.Email);

            //if (currentUser != null)
            //{
            //    currentUser.Country = item.Country;
            //    currentUser.DateOfBirth = item.DateOfBirth;
            //    currentUser.FirstName = item.FirstName;
            //    currentUser.LastName = item.LastName;
            //    currentUser.PostalCode = item.PostalCode;
            //}
        }
    }
}
