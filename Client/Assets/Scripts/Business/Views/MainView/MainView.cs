using System;
using System.Collections;
using System.Collections.Generic;
using DanielLochner.Assets.SimpleScrollSnap;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace FSL
{
    public interface IPage
    {
        public void Init();
    }

    public class MainView : BaseView
    {
        [SerializeField] private ScrollView _middelAreaScrollView;
        [SerializeField] private HomePage _homePage;

        private void Start()
        {
            Notifier.Trigger(FrameEvent.MenuTabClicked, EMenuTabType.Home);

            _homePage.Init();
        }
    }
}