using UnityEngine;

namespace FSL
{
    public class UserModule : Singleton<UserModule>
    {
        public override void Init()
        {
            base.Init();
            Notifier.Listen<string>(HttpRequestLabel.GET_PATH_GetUserByID, OnGetUserData);
        }

        private void OnGetUserData(string data)
        {
            // TODO 处理HTTP响应
            Debug.Log("获取的响应 [UserData]：" + data);
        }
    }
}