using System;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.Contracts
{
    public interface ISecurityLinkCommands
    {
        void SetNewShareLink(Guid profileId);
        void RemoveShareLink(Guid profileId);
    }
}
