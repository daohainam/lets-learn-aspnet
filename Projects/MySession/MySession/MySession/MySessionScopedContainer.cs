namespace MySession.MySession
{
    public class MySessionScopedContainer
    {
        public MySessionScopedContainer(ILogger<MySessionScopedContainer> logger) {
            logger.LogInformation("MySessionScopedContainer created");
        }

        public ISession? Session { get; set; }
    }
}
