using System;
using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

namespace FSL
{
    public static class ProtocolDataUtil
    {
        // 配置类对象 转 QueryData字符串，如： id=1&name=sl&age=18
        public static string ConfigToQueryDataString<T>(T data)
        {
            if (data == null) return string.Empty;

            var type = typeof(T);
            var keyValuePairs = new List<string>();

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
            foreach (var field in fields)
            {
                var value = field.GetValue(data);
                keyValuePairs.Add($"{field.Name}={Uri.EscapeDataString(value?.ToString() ?? string.Empty)}");
            }

            return string.Join("&", keyValuePairs);
        }

        public static T JsonToConfig<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ConfigToJson<T>(T data)
        {
            return JsonUtility.ToJson(data);
        }
    }
}