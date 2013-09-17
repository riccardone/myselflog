using System.Collections.Generic;
using System.Linq;
using MySelf.Diab.Data.Contracts;
using MySelf.Diab.Model;
using System.Data.Entity;

namespace MySelf.Diab.Data
{
    public class ModelReader : IModelReader
    {
        private readonly DiabDbContext _db;

        public ModelReader(IDatabaseFactory databaseFactory)
        {
            _db = databaseFactory.Get();
        }

        public bool UserExist(string email)
        {
            return _db.People.Any(u => u.Email.ToLower() == email);
        }

        public bool IsFriend(string email)
        {
            return
                _db.LogProfiles
                   .Include(f => f.Friends)
                   .Any(l => l.Friends.Any(f => f.Email == email));
        }

        public List<LogProfile> GetLogProfilesAsOwner(string email)
        {
            return
                _db.LogProfiles
                   .Include(g => g.GlucoseLevels)
                   .Include(o => o.Person).Include(s => s.SecurityLink)
                   .Where(g => g.Person.Email == email).ToList();
        }

        public List<LogProfile> GetLogProfilesAsFriend(string email)
        {
            return
                _db.LogProfiles.Include(g => g.GlucoseLevels)
                   .Include(f => f.Friends).Include(s => s.SecurityLink)
                   .Where(l => l.Friends.Any(f => f.Email == email))
                   .ToList();
        }


        public LogProfile GetLogProfile(System.Guid globalId)
        {
            return
                _db.LogProfiles
                .Include(e => e.Person)
                .Include(f => f.Friends)
                .Include(s => s.SecurityLink)
                .Include(g => g.GlucoseLevels)
                .FirstOrDefault(l => l.GlobalId == globalId);
        }


        public LogProfile GetLogProfile(string securityLink)
        {
            var profile = _db.LogProfiles
                .Include(e => e.Person)
                .Include(f => f.Friends)
                .Include(s => s.SecurityLink)
                .Include(g => g.GlucoseLevels)
                .FirstOrDefault(l => l.SecurityLink.Link == securityLink);

            if (profile != null)
                profile.GlucoseLevels = profile.GlucoseLevels.OrderBy(g => g.LogDate).ToList();
            return profile;
        }
    }
}
