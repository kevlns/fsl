using UnityEngine;

namespace FSL
{
    public class NetModule : Singleton<NetModule>
    {
        private GameObject _globalMonoGO;
        private ClientAgent _clientAgent = new ClientAgent();

        public static HttpService HttpHandle => Instance._clientAgent.httpService;
        public static WebSocketService WebSocketHandle => Instance._clientAgent.webSocketService;

        public void Tick()
        {
            _clientAgent.Tick();
        }

        public void ConnectWebSocket()
        {
            WebSocketHandle.Connect();
        }
    }
}