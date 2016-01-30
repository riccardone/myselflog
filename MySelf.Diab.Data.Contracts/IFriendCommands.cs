using System;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.Contracts
{
    public interface IFriendCommands
    {
        void Create(Friend item);
        void Delete(string email, Guid globalId);
    }
}
