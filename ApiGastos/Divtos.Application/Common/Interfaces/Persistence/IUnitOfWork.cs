namespace Divtos.Application.Common.Interfaces.Persistence
{
    public interface IUnitOfWork: IDisposable
    {
        IUserRepository Users { get; }
        IGroupRepository Groups { get; }
        Task<int> CompleteAsync();
    }
}
