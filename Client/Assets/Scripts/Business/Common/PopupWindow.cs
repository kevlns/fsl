using UnityEngine;
using UnityEngine.UI;

namespace FSL
{
    public class PopupWindow : MonoBehaviour, IUI
    {
        [SerializeField] private Image _image;
        [SerializeField] private Text _title;
        [SerializeField] private Text _desc;
        [SerializeField] private Button _closeButton;

        private void Start()
        {
            _closeButton.onClick.AddListener(() => Close());
        }

        public void Open(params object[] args)
        {
            gameObject.SetActive(true);
            if (args.Length != 2) return;
            _title.text = args[0] as string;
            _desc.text = args[1] as string;
        }

        public void Close()
        {
            gameObject.SetActive(false);
            Notifier.Trigger(UIEvent.NotifyOne);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}