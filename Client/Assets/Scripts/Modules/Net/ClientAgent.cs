
namespace FSL
{
    public class ClientAgent
    {
        public HttpService httpService = new HttpService();
        public WebSocketService webSocketService = new WebSocketService();

        public void Tick()
        {
            webSocketService.Tick();
        }
    }
}