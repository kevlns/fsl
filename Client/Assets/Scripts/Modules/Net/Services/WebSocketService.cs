using UnityEngine;
using NativeWebSocket;

namespace FSL
{
    public class WebSocketService
    {
        private WebSocket _websocket = null;
        private float _lastHeartbeatTime = 0f;
        private readonly float _heartbeatInterval = 5f; // 30秒发送一次心跳
        private bool _isConnected = false;

        public async void Connect()
        {
            _websocket = new WebSocket(ServerConfig.GetWebSocketServiceURL());
            _websocket.OnOpen += OnOpenCallBack;
            _websocket.OnError += OnErrorCallBack;
            _websocket.OnClose += OnCloseCallBack;
            _websocket.OnMessage += OnReceiveMessageCallBack;
            await _websocket.Connect();
        }

        public void Tick()
        {
            // 处理WebSocket消息队列和心跳
            if (!_isConnected) return;
#if !UNITY_WEBGL || UNITY_EDITOR
            _websocket?.DispatchMessageQueue();
#endif
            HandleHeartbeat();
        }

        private void OnOpenCallBack()
        {
            Debug.Log("连接成功!");
            _isConnected = true;
            _lastHeartbeatTime = Time.time;
        }

        private void OnErrorCallBack(string e)
        {
            Debug.LogError("连接失败： " + e);
            _isConnected = false;
        }

        private void OnCloseCallBack(WebSocketCloseCode code)
        {
            Debug.Log("连接关闭!");
            _isConnected = false;
        }

        private void OnReceiveMessageCallBack(byte[] bytes)
        {
            Debug.Log("接收到消息!");
            // 获取消息内容
            var message = System.Text.Encoding.UTF8.GetString(bytes);
            Debug.Log("Received message: " + message);

            // 检查是否是心跳响应
            if (message == "pong")
            {
                Debug.Log("Heartbeat response received");
            }
        }

        private void HandleHeartbeat()
        {
            if (_websocket.State != WebSocketState.Open) return;

            // 每隔一定时间发送心跳
            if (Time.time - _lastHeartbeatTime >= _heartbeatInterval)
            {
                SendHeartbeat();
                _lastHeartbeatTime = Time.time;
            }
        }

        private async void SendHeartbeat()
        {
            if (_websocket != null && _websocket.State == WebSocketState.Open)
            {
                Debug.Log("发送心跳...");
                await _websocket.SendText("ping");
            }
        }

        public async void SendWebSocketMessage()
        {
            if (_websocket != null && _websocket.State == WebSocketState.Open)
            {
                Debug.Log("Sending message...");

                // Sending bytes
                await _websocket.Send(new byte[] { 10, 20, 30 });

                // Sending plain text
                await _websocket.SendText("plain text message");
            }
        }

        private async void OnApplicationQuit()
        {
            if (_websocket != null)
            {
                await _websocket.Close();
            }
        }
    }
}