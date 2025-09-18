using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace FSL
{
    public class AssetModule : Singleton<AssetModule>
    {
        /**
         * 只用于加载随包资源 =================================================================
         */
        public void LoadPrefab(string prefabName, Action<GameObject> callback)
        {
            Addressables.LoadAssetAsync<GameObject>(prefabName).Completed += (handle) =>
            {
                if (handle.IsDone && handle.Status == AsyncOperationStatus.Succeeded)
                {
                    callback(handle.Result);
                    Debug.Log($"[LoadPrefab] - '{prefabName}' 加载完成");
                }
                else
                {
                    Debug.LogWarning($"[LoadPrefab] - '{prefabName}' 加载失败");
                }
            };
        }

        public void LoadSprite(string spriteName, Action<Sprite> callback)
        {
            Addressables.LoadAssetAsync<Sprite>(spriteName).Completed += (handle) =>
            {
                if (handle.IsDone && handle.Status == AsyncOperationStatus.Succeeded)
                {
                    callback(handle.Result);
                    Debug.Log($"[LoadSprite] - '{spriteName}' 加载完成");
                }
                else
                {
                    Debug.LogWarning($"[LoadSprite] - '{spriteName}' 加载失败");
                }
            };
        }
    }
}