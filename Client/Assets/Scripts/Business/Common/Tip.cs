using System;
using System.Collections;
using System.Collections.Generic;
using FSL;
using UnityEngine;
using UnityEngine.UI;

namespace FSL
{
    public enum TipType
    {
        Info,
        Warning,
        Error
    }

    public class Tip : MonoBehaviour
    {
        private static Dictionary<TipType, string> _tipType2IconName = new Dictionary<TipType, string>()
        {
            { TipType.Info, "tip-info" },
            { TipType.Warning, "tip-warning" },
            { TipType.Error, "tip-error" },
        };

        [SerializeField] private Text _title;
        private Image _icon;
        private Button _button;
        private string _popupTitle = "tip title";
        private string _popupDesc = "hello tip";

        private void Awake()
        {
            _icon = GetComponentInChildren<Image>();
            _button = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() => { OnClicked(); });
        }

        private void OnClicked()
        {
            Debug.Log($"[{gameObject.name}] Clicked");
            Global.UIManager.Open("PopupWindow", _popupTitle, _popupDesc);
        }

        // TODO 应该用配置文件来初始化组件
        public void Init(TipType type, string title, string popupTitle, string popupDesc)
        {
            Core.AssetModule.LoadSprite(_tipType2IconName[type], (sprite) => { _icon.sprite = sprite; });
            _title.text = title;
            _popupTitle = popupTitle;
            _popupDesc = popupDesc;
        }
    }
}