namespace MySelf.Diab.Data.Contracts
{
    public interface ILogManager : IUnitOfWork
    {
        ILogCommands LogCommands { get; }
        IPersonCommands PersonCommands { get; }
        IFriendCommands FriendCommands { get; }
        ISecurityLinkCommands SecurityLinkCommands { get; }
        IModelReader ModelReader { get; }
    }
}
