using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FSL
{
    public class UIManager : Singleton<UIManager>
    {
        private RectTransform _rootCanvas;

        public override void Init()
        {
            base.Init();
            _rootCanvas = GameObject.Find("RootCanvas").GetComponent<RectTransform>();
        }

        public void Open(string uiName, params object[] args)
        {
            Core.AssetModule.LoadPrefab(uiName, (prefab) =>
            {
                GameObject go = GameObject.Instantiate(prefab, _rootCanvas);
                IUI uiComponent = go.GetComponent<IUI>();
                uiComponent.Open(args);
            });
        }
    }
}