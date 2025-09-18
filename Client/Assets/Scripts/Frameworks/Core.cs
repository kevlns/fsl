using UnityEngine;

namespace FSL
{
    public class Core : SingletonMono<Core>
    {
        /**
        * 静态属性 =============================================================
        */
        public static AssetModule AssetModule => AssetModule.Instance;

        public static NetModule NetModule => NetModule.Instance;

        public static UserModule UserModule => UserModule.Instance;

        private void Awake()
        {
            AssetModule.Init();
            NetModule.Init();
            UserModule.Init();
        }

        private void Start()
        {
            // 延迟5s调用一个函数
            Invoke("Invoked", 5);
        }

        private void Invoked()
        {
            NetModule.ConnectWebSocket();
            NetModule.HttpHandle.SendRequest(HttpRequestLabel.GET_PATH_GetUserByID, 10);
            // NetModule.HttpHandle.SendRequest(HttpRequestLabel.POST_NewUser, new NewUserReqData()
            // {
            //     username = "sl"
            // });
        }

        private void Update()
        {
            NetModule.Tick();
        }
    }
}