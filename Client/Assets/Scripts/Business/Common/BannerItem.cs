using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BannerItem : MonoBehaviour
{
    [SerializeField] private Image _bg;
    [SerializeField] private Text _text;

    public void Init(string txt)
    {
        // TODO 添加数据
        _text.text = txt;
    }

    // Update is called once per frame
    void Update()
    {
    }
}