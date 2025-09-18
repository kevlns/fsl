using System;
using System.Collections;
using System.Collections.Generic;
using FSL;
using UnityEngine;
using UnityEngine.UI;

namespace FSL
{
    public enum ETipType
    {
        Info,
        Warning,
        Error
    }

    public class Tip : MonoBehaviour
    {
        private static Dictionary<ETipType, string> _tipType2IconName = new Dictionary<ETipType, string>()
        {
            { ETipType.Info, "tip-info" },
            { ETipType.Warning, "tip-warning" },
            { ETipType.Error, "tip-error" },
        };

        private Image _icon;
        private Button _button;
        private string _tipTitle = "tip title";
        private string _tipDesc = "hello tip";

        private void Awake()
        {
            _icon = GetComponent<Image>();
            _button = GetComponentInChildren<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(() => { OnClicked(); });
        }

        private void OnClicked()
        {
            Debug.Log($"[{gameObject.name}] Clicked");
            Global.UIManager.Open("PopupWindow", _tipTitle, _tipDesc);
        }

        // TODO 应该用配置文件来初始化组件
        // public void Init(Json json)
        // {
        //     
        // }
    }
}