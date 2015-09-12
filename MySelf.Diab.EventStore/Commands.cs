using EventStore.ClientAPI;
using MySelf.Diab.Data.Contracts;
using Newtonsoft.Json;
using System;

namespace MySelf.Diab.EventStore
{
    public class Commands : ILogCommands
    {
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly JsonSerializerSettings SerializerSettings;

        public Commands()
        {
            SerializerSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.None };
        }

        public void AddLogMessage(Model.LogMessage logMessage)
        {
            throw new NotImplementedException();
        }

        public void AddLogProfile(Diab.Model.LogProfile logProfile)
        {
            throw new NotImplementedException();
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
