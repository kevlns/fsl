using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
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
        private HorizontalLayoutGroup _layout;
        [SerializeField] private EMenuTabType _menuType;

        private Vector3 _defaultScale = Vector3.one;
        private Vector3 _selectedScale = new Vector3(1.05f, 1.05f, 1.05f);

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
            _bg = _button.GetComponent<Image>();
            _text = _button.GetComponentInChildren<Text>();
            _layout = transform.parent.GetComponent<HorizontalLayoutGroup>();
            Notifier.Listen<EMenuTabType>(FrameEvent.MenuTabClicked, OnEventMenuTabClicked);
        }

        private void Start()
        {
            _button.onClick.AddListener(() => { OnClicked(); });
        }

        private void OnClicked()
        {
            Debug.Log($"点击了 [{_text.text}] 菜单标签");
            _isSelected = true;
            Notifier.Trigger(FrameEvent.MenuTabClicked, _menuType);
        }

        private void OnEventMenuTabClicked(EMenuTabType type)
        {
            _isSelected = (type == _menuType);
            _rect.localScale = _isSelected ? _selectedScale : _defaultScale;
            _layout.enabled = false;
            _layout.enabled = true;
        }
    }
}