using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FSL
{
    public class UIManager : Singleton<UIManager>
    {
        public override void Init()
        {
            base.Init();
        }

        public void Open(string uiName, params object[] args)
        {
            Core.AssetModule.LoadPrefab(uiName, (prefab) =>
            {
                GameObject go = GameObject.Instantiate(prefab, Global.GetRootCanvasTransform());
                IUI uiComponent = go.GetComponent<IUI>();
                uiComponent.Open(args);
            });
        }
    }
}