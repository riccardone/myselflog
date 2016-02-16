using System;
using MySelf.Diab.Model;

namespace MySelf.Diab.Data.Contracts
{
    public interface ILogCommands
    {
        void AddLogProfile(LogProfile logProfile);
        void AddLogProfile(string name, Guid globalId, string email);
        void AddLogProfile(string name, Guid globalId, Person owner);
        void AddLogMessage(LogMessage logMessage);
        void Delete(Guid globalid);
        void DeleteTerapy(Guid globalid);
    }
}
