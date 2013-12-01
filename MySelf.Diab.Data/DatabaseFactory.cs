using MySelf.Diab.Data.Contracts;

namespace MySelf.Diab.Data
{
    public class DatabaseFactory : IDatabaseFactory
    {
        private DiabDbContext _dbContext;

        public dynamic Get()
        {
            return _dbContext ?? (_dbContext = new DiabDbContext());
        }
    }
}
