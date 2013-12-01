namespace MySelf.Diab.Core.Contracts
{
    public interface ICryptoService
    {
        string GenerateKey();
        string GenerateKey(string name);
    }
}
