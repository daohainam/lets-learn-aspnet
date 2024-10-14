


namespace MySession.MySession
{
    public interface IMySessionStorageEngine
    {
        Task CommitAsync(string id, Dictionary<string, byte[]> _store, CancellationToken cancellationToken);
        Dictionary<string, byte[]> Load(string id);
        Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken);
    }
}
