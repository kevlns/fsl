using UnityEngine;
using UnityEngine.UI;

public class ModList : MonoBehaviour
{
    private RectTransform _rectTransform;
    private VerticalLayoutGroup _layout;
    private Button _moreModbutton;
    private RectTransform _contentGridTrans;
    private GridLayoutGroup _contentGridLayout;
    private int _cardCount = 0;
    private float _bottomFixedOffset = 20f;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _moreModbutton = GameObject.Find("MoreModButton").GetComponent<Button>();
        _moreModbutton.onClick.AddListener(OnMoreModClicked);
        _contentGridTrans = GameObject.Find("GridLayout").GetComponent<RectTransform>();
        _contentGridLayout = _contentGridTrans.GetComponent<GridLayoutGroup>();
    }

    public void Add(GameObject prefab, string title, string desc)
    {
        var go = Instantiate(prefab, _contentGridTrans);
        go.GetComponent<ModCard>().Init(title, desc);
        ++_cardCount;

        float height = Mathf.Ceil(_cardCount / 2f) * _contentGridLayout.cellSize.y + _contentGridLayout.spacing.y;
        float dy = height - _contentGridTrans.sizeDelta.y;
        _contentGridTrans.sizeDelta = new Vector2(_contentGridTrans.sizeDelta.x, height);
        _rectTransform.sizeDelta =
            new Vector2(_rectTransform.sizeDelta.x, _rectTransform.sizeDelta.y + dy + _bottomFixedOffset);
        _bottomFixedOffset = 0;
    }

    private void OnMoreModClicked()
    {
        //TODO
        Debug.Log("【官方模组】点击了更多信息");
    }
}