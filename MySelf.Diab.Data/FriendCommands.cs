using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data
{
    public class FriendCommands : IFriendCommands 
    {
        private readonly DiabDbContext _db;

        public FriendCommands(IDatabaseFactory databaseFactory)
        {
            _db = databaseFactory.Get();
        }

        public void Create(Friend item)
        {
            _db.Friends.Add(item);
        }
    }
}
