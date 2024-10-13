

namespace MySession.MySession
{
    public interface IMySessionStorageEngine
    {
        Task CommitAsync(string id, Dictionary<string, byte[]> _store, CancellationToken cancellationToken);
        Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken);
    }
}
