using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FSL
{
    public enum UIEvent
    {
        NotifyOne
    }

    public class UIManager : Singleton<UIManager>
    {
        private Stack<GameObject> _blockedUI = new Stack<GameObject>();
        private GameObject _currentUI = null;
        private GameObject _toDelUI = null;

        public override void Init()
        {
            base.Init();
            Notifier.Listen(UIEvent.NotifyOne, OnNotifyOne);
        }

        public void Open(string uiName, params object[] args)
        {
            Core.AssetModule.LoadPrefab(uiName, (prefab) =>
            {
                if (_currentUI)
                {
                    _currentUI.SetActive(false);
                    _blockedUI.Push(_currentUI);
                }

                GameObject go = GameObject.Instantiate(prefab, Global.GetRootCanvasTransform());
                IUI ui = go.GetComponent<IUI>();
                ui.Open(args);
                _currentUI = go;
            });
        }

        private void OnNotifyOne()
        {
            _toDelUI = _currentUI;
            _currentUI = null;
            if (_blockedUI.Count > 0)
            {
                _currentUI = _blockedUI.Pop();
                _currentUI.SetActive(true);
            }
        }

        public void LateUpdate()
        {
            if (_toDelUI)
            {
                GameObject.Destroy(_toDelUI);
                _toDelUI = null;
            }
        }
    }
}