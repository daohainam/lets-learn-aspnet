namespace MySession.MySession
{
    public interface IMySessionStorage
    {
        ISession Create();
        ISession Get(string id);
    }
}
