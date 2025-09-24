using UnityEngine;

namespace FSL
{
    public class Global : SingletonMono<Global>
    {
        /**
         * 静态属性 =============================================================
         */

        public static UIManager UIManager => UIManager.Instance;

        public static ViewManager ViewManager => ViewManager.Instance;

        private Canvas _rootCanvas;
        private RectTransform _rootCanvasTransform;

        private void Awake()
        {
            _rootCanvas = GameObject.Find("RootCanvas").GetComponent<Canvas>();
            Global.UIManager.Init();
            Global.ViewManager.Init();
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            // 驱动其他模块的帧更新
        }

        public static Transform GetRootCanvasTransform()
        {
            return Instance._rootCanvasTransform;
        }
    }
}