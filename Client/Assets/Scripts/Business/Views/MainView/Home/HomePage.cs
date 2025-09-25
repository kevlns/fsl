using DanielLochner.Assets.SimpleScrollSnap;
using FSL;
using UnityEngine;
using UnityEngine.UI;

public class HomePage : MonoBehaviour
{
    private ContentSizeFitter _contentSizeFitter;
    private bool _isInit = false;
    public bool IsInit => _isInit;

    // top: banner related
    private SimpleScrollSnap _bannerScrollView;

    // private GameObject _bannerCardPrefab;
    private readonly float _autoScrollCd = 5f;
    private float _autoScrollTimer;
    private bool _bannerInit = false;
    private int _bannerIndex = 0;
    private int _bannerCardCount = 0;

    // bottom: info list related
    // private GameObject _infoCardPrefab;
    private InfoList _infoList;
    private ModList _modList;

    private void Update()
    {
        AutoScroll();
    }

    public void Init()
    {
        _contentSizeFitter = GetComponent<ContentSizeFitter>();
        _autoScrollTimer = _autoScrollCd;

        // 填充顶部 Banner 滚板
        _bannerScrollView = GameObject.Find("BannerScrollView").GetComponent<SimpleScrollSnap>();
        Core.AssetModule.LoadPrefab("BannerCard", SetupBanner);

        // 填充底部 Info 列表
        _infoList = GetComponentInChildren<InfoList>();
        Core.AssetModule.LoadPrefab("InfoCard", SetupInfoList);

        // 填充底部 Mod 列表
        _modList = GetComponentInChildren<ModList>();
        Core.AssetModule.LoadPrefab("ModCard", SetupModList);
    }

    private void SetupBanner(GameObject prefab)
    {
        // TODO 根据服务器的剧本数据进行初始化
        for (int i = 0; i < 3; i++)
        {
            _bannerScrollView.AddToBack(prefab);
            var banner = _bannerScrollView.Panels[i].GetComponent<BannerCard>();
            banner.Init(i.ToString());
            ++_bannerCardCount;
        }

        _bannerScrollView.onPanelCentered.AddListener((cInd, sInd) => { _bannerIndex = cInd; });
        _bannerInit = true;
    }

    private void SetupInfoList(GameObject prefab)
    {
        // TODO 根据服务器的设定集数据进行初始化，默认显示两个
        for (int i = 0; i < 3; i++)
        {
            _infoList.Add(prefab, i.ToString());
        }

        _contentSizeFitter.enabled = false;
        _contentSizeFitter.enabled = true;
    }

    private void SetupModList(GameObject prefab)
    {
        // TODO 根据服务器的设定集数据进行初始化，默认显示两个
        for (int i = 0; i < 4; i++)
        {
            _modList.Add(prefab, i.ToString(), i.ToString());
        }

        _contentSizeFitter.enabled = false;
        _contentSizeFitter.enabled = true;
    }

    private void AutoScroll()
    {
        if (!_bannerInit) return;

        _autoScrollTimer -= Time.deltaTime;
        if (_autoScrollTimer < 0)
        {
            _autoScrollTimer = _autoScrollCd;
            _bannerIndex = (_bannerIndex + 1) % _bannerCardCount;
            _bannerScrollView.GoToPanel(_bannerIndex);
        }
    }
}