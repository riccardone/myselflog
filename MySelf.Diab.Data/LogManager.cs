using MySelf.Diab.Core.Contracts;
using MySelf.Diab.Data.Contracts;

namespace MySelf.Diab.Data
{
    public class LogManager : ILogManager
    {
        private readonly DiabDbContext _dbContext;

        public int Save()
        {
            return _dbContext.SaveChanges();
        }

        public LogManager(IDatabaseFactory databaseFactory, ICryptoService cryptoService)
        {
            _dbContext = databaseFactory.Get() as DiabDbContext;
            LogCommands = new LogCommands(databaseFactory);
            ModelReader = new ModelReader(databaseFactory);
            PersonCommands = new PersonCommands(databaseFactory);
            FriendCommands = new FriendCommands(databaseFactory);
            SecurityLinkCommands = new SecurityLinkCommands(databaseFactory, cryptoService);
        }

        public ILogCommands LogCommands { get; private set; }

        public IPersonCommands PersonCommands { get; private set; }

        public IModelReader ModelReader { get; private set; }

        public IFriendCommands FriendCommands { get; private set; }

        public ISecurityLinkCommands SecurityLinkCommands { get; private set; }
    }
}
