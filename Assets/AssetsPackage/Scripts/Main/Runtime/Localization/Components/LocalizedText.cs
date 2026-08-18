using UnityEngine;
using UnityEngine.UI;

namespace Framework.Localization.Components
{
    /// <summary>只保存 Key 的 UGUI Text 本地化组件。</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public sealed class LocalizedText : MonoBehaviour
    {
        [SerializeField] private string key;
        private Text target;

        public string Key
        {
            get => key;
            set { key = value; Refresh(); }
        }

        private void Awake() => target = GetComponent<Text>();
        private void OnEnable()
        {
            LocalizationService.Instance.LocaleChanged += OnLocaleChanged;
            Refresh();
        }
        private void OnDisable() => LocalizationService.Instance.LocaleChanged -= OnLocaleChanged;
        private void OnLocaleChanged(string _) => Refresh();
        public void Refresh()
        {
            if (target == null) target = GetComponent<Text>();
            if (target != null && !string.IsNullOrWhiteSpace(key)) target.text = L10n.Get(key);
        }
    }
}
