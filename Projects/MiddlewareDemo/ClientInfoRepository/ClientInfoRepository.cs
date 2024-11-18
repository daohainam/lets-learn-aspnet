namespace MiddlewareDemo.ClientInfoRepository
{
    public class ClientInfoRepository : IClientInfoRepository
    {
        public ClientInfo? GetClientInfo(string apiKey)
        {
            if (_clientInfos.ContainsKey(apiKey))
            {
                return _clientInfos[apiKey];
            }

            return null;
        }

        private Dictionary<string, ClientInfo> _clientInfos = new Dictionary<string, ClientInfo>
        {
            { "123", new ClientInfo { Id = 1, Name = "Client 1" } },
            { "456", new ClientInfo { Id = 2, Name = "Client 2" } },
            { "789", new ClientInfo { Id = 3, Name = "Client 3" } }
        };
    }
}
