using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace FSL
{
    public enum UIEvent
    {
        CloseUI,
        BlockUI
    }

    public class UIManager : Singleton<UIManager>
    {
        private Queue<IUI> _uiToDestroy = new Queue<IUI>();
        private Stack<IUI> _uiToBlock = new Stack<IUI>();

        public override void Init()
        {
            base.Init();
            Notifier.Listen<IUI>(UIEvent.CloseUI, ToDestroyUI);
            Notifier.Listen<IUI>(UIEvent.BlockUI, BlockUI);
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

        private void BlockUI(IUI ui)
        {
            ui.GetGameObject().SetActive(false);
            _uiToBlock.Push(ui);
        }

        private void ToDestroyUI(IUI ui)
        {
            _uiToDestroy.Enqueue(ui);
        }

        public void LateUpdate()
        {
            while (_uiToDestroy.Count > 0)
            {
                IUI ui = _uiToDestroy.Dequeue();
                if (ui != null && ui.GetGameObject() != null)
                {
                    GameObject.Destroy(ui.GetGameObject());
                }
            }
        }
    }
}