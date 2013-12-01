using MySelf.Diab.Model;

namespace MySelf.Diab.Data.Contracts
{
    public interface IPersonCommands
    {
        void Create(Person item);
        void Update(Person item);
        //void CreateLogProfile(string email, string logProfileName);
    }
}
