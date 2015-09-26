using CrossCutting.Repository;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Domain.Aggregates;
using System;

namespace MySelf.Diab.EventStore
{
    public class EventStoreLogCommands : ILogCommands
    {
        private IDomainRepository _repository;

        public EventStoreLogCommands(IDomainRepository repository)
        {
            _repository = repository;
        }

        public void AddLogMessage(Model.LogMessage message)
        {
            var logProfile = _repository.GetById<LogProfile>(message.ProfileId.ToString());
            logProfile.LogValue(message.Message, message.Value, message.LogDate);
            _repository.Save(logProfile);
        }

        public void AddLogProfile(Model.LogProfile message)
        {
            var logProfile = LogProfile.CreateLogProfile(message.GlobalId.ToString(), message.Name, message.Person.Id.ToString());
            var result = _repository.Save(logProfile, true);
        }

        public void AddLogProfile(string name, Guid globalId, Diab.Model.Person owner)
        {
            throw new NotImplementedException();
        }

        public void AddLogProfile(string name, Guid globalId, string email)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid globalid)
        {
            throw new NotImplementedException();
        }

        public void DeleteTerapy(Guid globalid)
        {
            throw new NotImplementedException();
        }
    }
}
