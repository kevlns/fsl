using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace FSL
{
    public class HttpService
    {
        private HttpClient _httpClient = new HttpClient();

        private async Task<string> HttpGet(string url)
        {
            try
            {
                return await _httpClient.GetStringAsync(url);
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"GET请求失败: {ex.Message}");
                return null;
            }
        }

        private async Task<string> HttpPost(string url, string jsonData)
        {
            try
            {
                var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(url, content);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"POST请求失败: {ex.Message}");
                return null;
            }
        }

        private void HandleHttpResponse(string response, HttpRequestLabel requestLabel)
        {
            Notifier.Trigger(requestLabel, response);
        }

        private string BuildURL<T>(HttpRequestLabel requestLabel, T data)
        {
            string url = HttpRequestConfig.GetFullUrl(requestLabel);
            int requestIndex = (int)requestLabel;
            if (requestIndex <= HttpRequestConfig.GetPathIndexRange) // GET_PATH
            {
                url = url.Replace("{param}", data?.ToString() ?? string.Empty);
            }
            else if (requestIndex <= HttpRequestConfig.GetQueryIndexRange) // GET_QUERY 
            {
                url += ProtocolDataUtil.ConfigToQueryDataString(data);
            }

            return url;
        }

        // 在ClientAgent中使用
        public void SendRequest<T>(HttpRequestLabel requestLabel, T data)
        {
            string url = BuildURL(requestLabel, data);
            int requestIndex = (int)requestLabel;
            if (requestIndex <= HttpRequestConfig.GetIndexRange)
            {
                url += ProtocolDataUtil.ConfigToQueryDataString(data);
                HttpGet(url).ContinueWith(task =>
                {
                    if (task.Result != null)
                    {
                        HandleHttpResponse(task.Result, requestLabel);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                string jsonData = ProtocolDataUtil.ConfigToJson(data);
                HttpPost(url, jsonData).ContinueWith(task =>
                {
                    if (task.Result != null)
                    {
                        HandleHttpResponse(task.Result, requestLabel);
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}