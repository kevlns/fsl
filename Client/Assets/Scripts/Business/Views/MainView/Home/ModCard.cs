using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModCard : MonoBehaviour
{
    [SerializeField] private Image _cardBg;
    [SerializeField] private Image _cover;
    [SerializeField] private Text _title;
    [SerializeField] private Text _desc;
    [SerializeField] private Button _button;

    // TODO 传入结构体参数
    public void Init(string title, string desc)
    {
        _button.onClick.AddListener(OnClick);
        _title.text = title;
        _desc.text = desc;
    }

    private void OnClick()
    {
        //TODO
        Debug.Log("【模组卡片】点击了模组卡片: " + _title.text);
    }
}