using UnityEngine;
using UnityEngine.UI;

public class InfoCard : MonoBehaviour
{
    private Text _title;
    private Image _cardImage;
    private Button _button;

    private void Awake()
    {
        _cardImage = GetComponent<Image>();
        _title = GetComponentInChildren<Text>();
        _button = GetComponent<Button>();
    }

    // TODO 传入结构体参数
    public void Init(string title)
    {
        _title.text = title;
    }

    private void OnClick()
    {
    }
}