using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FSL
{
    public enum EMenuTabType
    {
        Home,
        Channel,
        Role,
        Profile
    }

    public class MenuTab : MonoBehaviour
    {
        private RectTransform _rect;
        private Button _button;
        private Image _bg;
        private Text _text;
        private bool _isSelected = false;
        [SerializeField] private EMenuTabType menuType;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
            _bg = _button.GetComponent<Image>();
            _text = _button.GetComponentInChildren<Text>();
            _isSelected = (menuType == EMenuTabType.Home);
        }

        private void Start()
        {
            _button.onClick.AddListener(() => { OnClicked(); });
            UseActiveScaleSetting(_isSelected);
        }

        private void UseActiveScaleSetting(bool flag)
        {
            if (flag)
            {
                _rect.localScale = new Vector3(1.2f, 1.1f, 1f);
            }
            else
            {
                _rect.localScale = Vector3.one;
            }
        }

        private void OnClicked()
        {
            Debug.Log($"点击了 [{_text.text}] 菜单标签");
        }
    }
}