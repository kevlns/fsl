using UnityEngine;
using UnityEngine.UI;

public class InfoList : MonoBehaviour
{
    private RectTransform _rectTransform;
    private VerticalLayoutGroup _layout;
    private Button _moreInfobutton;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _layout = GetComponent<VerticalLayoutGroup>();
        _moreInfobutton = GameObject.Find("MoreInfoButton").GetComponent<Button>();
        _moreInfobutton.onClick.AddListener(OnMoreInfoClicked);
    }

    // TODO 传入结构体参数
    public void Add(GameObject prefab, string title)
    {
        var go = Instantiate(prefab, _rectTransform);
        go.GetComponent<InfoCard>().Init(title);

        float dy = go.GetComponent<RectTransform>().rect.height + _layout.spacing;
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y + dy);
    }

    private void OnMoreInfoClicked()
    {
        //TODO
        Debug.Log("【资讯列表】点击了更多信息");
    }
}