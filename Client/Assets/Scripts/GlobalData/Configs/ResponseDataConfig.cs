using System;
using System.Reflection;
using System.Collections.Generic;

namespace FSL
{
    [Serializable]
    public class User_ResData
    {
        public string userId = "0";
        public string userName = "";
        public int vipLevel = 0;
    }

    [Serializable]
    public class Role_ResData
    {
        public string roleId = "0";
        public string roleName = "";
        public int roleLevel = 1;
    }
}