using UnityEngine;
using UnityEngine.UI;

public class BannerCard : MonoBehaviour
{
    private Image _image;
    private Text _text;
    private Button _button;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _text = GetComponentInChildren<Text>();
        _button = GetComponent<Button>();

        _button.onClick.AddListener(OnClick);
    }

    public void Init(string txt)
    {
        // TODO 添加数据
        _text.text = txt;
    }

    private void OnClick()
    {
        // TODO 添加点击事件
    }
}