using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FSL
{
    public class MainView : BaseView
    {
        [SerializeField] private Tip _serverTip;
        [SerializeField] private Text _serverName;
        [SerializeField] private MenuTab[] _menuTabs;

        [SerializeField] private HomePage _homePage;
        // private GameObject _channelPage;
        // private GameObject _rolePagePrefab;
        // private GameObject _profilePagePrefab;

        private void Start()
        {
            Notifier.Trigger(FrameEvent.MenuTabClicked, EMenuTabType.Home);

            _homePage.Init();
        }
    }
}