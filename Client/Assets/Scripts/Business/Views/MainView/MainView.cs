using UnityEngine;
using UnityEngine.UI;

namespace FSL
{
    public class MainView : BaseView
    {
        private HomePage _homePage;
        private Image _image;
        [SerializeField] private RectTransform _topLayerTrans;
        [SerializeField] private RectTransform _middleLayerTrans;
        [SerializeField] private RectTransform _bottomLayerTrans;

        private void Awake()
        {
            _homePage = GetComponentInChildren<HomePage>();
            _image = GetComponent<Image>();
        }

        private void Start()
        {
            Notifier.Trigger(FrameEvent.MenuTabClicked, EMenuTabType.Home);

            _homePage.Init();
        }
    }
}