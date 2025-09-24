using System;
using System.Collections;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using FSL;
using UnityEngine;

public class HomePage : MonoBehaviour, IPage
{
    [SerializeField] private SimpleScrollSnap _bannerScrollView;

    private GameObject _bannerItem;
    private readonly float _autoScrollCd = 5f;
    private float _autoScrollTimer;

    private bool _bannerInit = false;

    private int _bannerIndex = 0;
    private int _bannerItemCount = 0;

    public void Init()
    {
        _autoScrollTimer = _autoScrollCd;
        Core.AssetModule.LoadPrefab("BannerItem", (prefab) =>
        {
            _bannerItem = prefab;
            InitBanner();
        });
    }

    private void Update()
    {
        AutoScroll();
    }

    private void InitBanner()
    {
        // TODO 根据服务器的剧本数据进行初始化
        for (int i = 0; i < 3; i++)
        {
            _bannerScrollView.AddToBack(_bannerItem);
            var banner = _bannerScrollView.Panels[i].GetComponent<BannerItem>();
            banner.Init(i.ToString());
            ++_bannerItemCount;
        }

        _bannerScrollView.onPanelCentered.AddListener((cInd, sInd) => { _bannerIndex = cInd; });
        _bannerInit = true;
    }

    private void AutoScroll()
    {
        if (!_bannerInit) return;

        _autoScrollTimer -= Time.deltaTime;
        if (_autoScrollTimer < 0)
        {
            _autoScrollTimer = _autoScrollCd;
            _bannerIndex = (_bannerIndex + 1) % _bannerItemCount;
            _bannerScrollView.GoToPanel(_bannerIndex);
        }
    }
}