
namespace MySession.MySession
{
    public class MySessionStorage : IMySessionStorage
    {
        private readonly IMySessionStorageEngine engine;
        private readonly Dictionary<string, ISession> sessions = new Dictionary<string, ISession>();

        public MySessionStorage(IMySessionStorageEngine engine)
        {
            this.engine = engine;
        }

        public ISession Create()
        {
            var newSession = new MySession(Guid.NewGuid().ToString("N"), engine);
            sessions[newSession.Id] = newSession;

            return newSession;
        }

        public ISession Get(string id)
        {
            if (sessions.ContainsKey(id))
            {
                return sessions[id];
            }

            return Create();            
        }
    }
}
