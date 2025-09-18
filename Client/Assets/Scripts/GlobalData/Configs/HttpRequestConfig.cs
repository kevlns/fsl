using System;
using System.Reflection;
using System.Collections.Generic;

namespace FSL
{
    // 【命名约定】：
    //      1. 只含有一个路径参数的GET请求，使用 GET_PATH_XXX
    //      2. 含有多个查询参数的GET请求，使用 GET_QUERY_XXX
    //      3. POST请求，使用 POST_XXX
    //      4. PUT请求，使用 PUT_XXX
    // 【数值约定】：
    //      1. 0-1000 为 GET_PATH 请求
    //      2. 1001-2000 为 GET_QUERY 请求
    //      3. 2001-3000 为 POST 请求
    public enum HttpRequestLabel
    {
        // GET_PATH 请求 ===================================
        GET_PATH_START = 0,
        GET_PATH_GetUserByID = 1,

        // GET_QUERY 请求 ==================================
        GET_QUERY_START = 1001,

        // POST 请求 =======================================
        POST_START = 2001,
        POST_NewUser = 2002
    }

    public static class HttpRequestConfig
    {
        public static int GetPathIndexRange = 1000;
        public static int GetQueryIndexRange = 2000;
        public static int PostIndexRange = 3000;
        public static int GetIndexRange = 2000;

        // 注：GET_PATH 请求的参数，统一使用 {param}
        private static Dictionary<HttpRequestLabel, string> _relativeUrls = new Dictionary<HttpRequestLabel, string>()
        {
            // 示例 ======================================================
            { HttpRequestLabel.GET_PATH_START, "/api/example/{param}" },
            { HttpRequestLabel.GET_QUERY_START, "/api/example?" },
            { HttpRequestLabel.POST_START, "/api/example" },
            // ==========================================================

            { HttpRequestLabel.GET_PATH_GetUserByID, "/api/users/{param}" },
            { HttpRequestLabel.POST_NewUser, "/api/users" },
        };

        public static string GetFullUrl(HttpRequestLabel label)
        {
            return ServerConfig.GetHttpServiceURL() + _relativeUrls[label];
        }
    }
}