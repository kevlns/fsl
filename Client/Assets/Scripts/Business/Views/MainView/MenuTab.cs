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
        private RectTransform _rectTransform;
        private Button _button;
        private Image _image;
        private Text _text;
        private bool _isSelected = false;
        private HorizontalLayoutGroup _pLayout;
        [SerializeField] private EMenuTabType _menuType;

        private Vector3 _defaultScale = Vector3.one;
        private Vector3 _selectedScale = new Vector3(1.05f, 1.05f, 1.05f);

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _button = GetComponent<Button>();
            _image = _button.GetComponent<Image>();
            _text = _button.GetComponentInChildren<Text>();
            _pLayout = transform.parent.GetComponent<HorizontalLayoutGroup>();
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
            _rectTransform.localScale = _isSelected ? _selectedScale : _defaultScale;
            _pLayout.enabled = false;
            _pLayout.enabled = true;
        }
    }
}