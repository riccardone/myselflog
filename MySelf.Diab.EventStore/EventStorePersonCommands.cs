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
            var currentUser = _repository.GetById<Domain.Aggregates.Person>(item.UniqueId);
            currentUser.UpdatePerson(item.FirstName, item.LastName, item.Email, item.Country, item.PostalCode);
            _repository.Save(currentUser);
        }
    }
}
