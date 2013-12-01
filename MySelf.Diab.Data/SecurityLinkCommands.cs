using System;
using System.Linq;
using MySelf.Diab.Core.Contracts;
using MySelf.Diab.Data.Contracts;
using System.Data.Entity;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data
{
    public class SecurityLinkCommands : ISecurityLinkCommands
    {
        private readonly ICryptoService _cryptoService;
        private readonly DiabDbContext _db;

        public SecurityLinkCommands(IDatabaseFactory databaseFactory, ICryptoService cryptoService)
        {
            _cryptoService = cryptoService;
            _db = databaseFactory.Get();
        }
        
        private bool LinkExist(string link)
        {
            return _db.SecurityLinks.Any(l => l.Link == link);
        }

        public void SetNewShareLink(Guid profileId)
        {
            var logProfile = _db.LogProfiles.Include(s => s.SecurityLink).FirstOrDefault(lp => lp.GlobalId == profileId);
            if (logProfile == null)
                throw new ArgumentException("profile not found");
            while (true)
            {
                var linkToAdd = _cryptoService.GenerateKey();
                if (LinkExist(linkToAdd))
                    continue;
                if (logProfile.SecurityLink == null)
                {
                    var securityLinkToAdd = new SecurityLink { Link = _cryptoService.GenerateKey(logProfile.Name) };
                    logProfile.SecurityLink = securityLinkToAdd;
                }
                else
                    logProfile.SecurityLink.Link = linkToAdd;    
                return;
            }
        }

        public void RemoveShareLink(Guid profileId)
        {
            var logProfile = _db.LogProfiles.Include(s => s.SecurityLink).FirstOrDefault(lp => lp.GlobalId == profileId);
            if (logProfile == null)
                throw new ArgumentException("profile not found");
            if (logProfile.SecurityLink != null)
                _db.SecurityLinks.Remove(logProfile.SecurityLink);
        }
    }
}
