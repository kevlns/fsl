using UnityEngine;
using UnityEngine.UI;

namespace FSL
{
    public class MainView : BaseView
    {
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _topLayerTrans;
        [SerializeField] private RectTransform _middleLayerTrans;
        [SerializeField] private RectTransform _bottomLayerTrans;

        // Home
        [SerializeField] private GameObject _homeContent;
        [SerializeField] private HomePage _homePage;

        // Channel
        [SerializeField] private GameObject _channelContent;
        [SerializeField] private ChannelPage _channelPage;

        private void Awake()
        {
            Notifier.Listen<EMenuTabType>(FrameEvent.MenuTabClicked, OnMenuTabClicked);
        }

        private void Start()
        {
            Notifier.Trigger(FrameEvent.MenuTabClicked, EMenuTabType.Home);

            _homePage.Init();
            _channelPage.Init();
        }

        private void OnMenuTabClicked(EMenuTabType type)
        {
            _homeContent.SetActive(false);
            _channelContent.SetActive(false);
            switch (type)
            {
                case EMenuTabType.Home:
                    _homeContent.SetActive(true);
                    break;
                case EMenuTabType.Channel:
                    _channelContent.SetActive(true);
                    break;
            }
        }
    }
}