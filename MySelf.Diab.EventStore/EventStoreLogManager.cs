using CrossCutting.Repository;
using MySelf.Diab.Data;
using MySelf.Diab.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Diab.EventStore
{
    public class EventStoreLogManager : ILogManager
    {        
        private ILogCommands _logCommands;
        private IFriendCommands _friendCommands;
        private IModelReader _modelReader;
        private IPersonCommands _personCommands;
        private ISecurityLinkCommands _securityLinkCommands;

        public EventStoreLogManager(IModelReader modelReader, IDomainRepository repository)
        {
            _logCommands = new EventStoreLogCommands(repository);
            _personCommands = new EventStorePersonCommands(repository);
            _modelReader = modelReader;
        }

        public IFriendCommands FriendCommands
        {
            get
            {
                return _friendCommands;
            }
        }

        public ILogCommands LogCommands
        {
            get
            {
                return _logCommands;
            }
        }

        public IModelReader ModelReader
        {
            get
            {
                return _modelReader;
            }
        }

        public IPersonCommands PersonCommands
        {
            get
            {
                return _personCommands;
            }
        }

        public ISecurityLinkCommands SecurityLinkCommands
        {
            get
            {
                return _securityLinkCommands;
            }
        }

        public int Save()
        {
            return int.MinValue;
        }
    }
}
