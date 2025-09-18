
namespace FSL
{
    public class Global : SingletonMono<Global>
    {
        /**
         * 静态属性 =============================================================
         */
        public static UIManager UIManager => UIManager.Instance;

        public static ViewManager ViewManager => ViewManager.Instance;

        private void Awake()
        {
            Global.UIManager.Init();
            Global.ViewManager.Init();
        }

        private void Update()
        {
            // 驱动其他模块的帧更新
        }
    }
}