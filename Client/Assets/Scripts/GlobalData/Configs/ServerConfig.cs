using System;

namespace FSL
{
    // 静态配置，用来获取服务器地址，TODO 可通过预先init从配置文件中读取
    public static class ServerConfig
    {
        private static readonly string _serverIP = "8.156.73.153";
        private static readonly string _serverPORT = ":80";
        private static readonly string _wsPreFix = "ws://";
        private static readonly string _wsPostFix = "/ws/websocket";
        private static readonly string _httpPreFix = "http://";

        public static string GetWebSocketServiceURL()
        {
            return _wsPreFix + _serverIP + _serverPORT + _wsPostFix;
        }

        public static string GetHttpServiceURL()
        {
            return _httpPreFix + _serverIP + _serverPORT;
        }
    }
}