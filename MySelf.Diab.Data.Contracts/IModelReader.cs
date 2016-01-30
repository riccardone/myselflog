using System;
using System.Collections.Generic;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.Contracts
{
    public interface IModelReader
    {
        bool UserExist(string email);
        List<LogProfile> GetLogProfilesAsOwner(string email);
        List<LogProfile> GetLogProfilesAsFriend(string email);
        bool IsFriend(string email);
        LogProfile GetLogProfile(Guid globalId);
        LogProfile GetLogProfile(string securityLink);
        Person GetPerson(string username);
    }
}
