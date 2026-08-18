using TMPro;
using UnityEngine;

namespace Framework.Localization.Components
{
    /// <summary>只保存 Key，并随语言快照切换字体的 TMP 组件。</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(TMP_Text))]
    public sealed class LocalizedTMPText : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private bool applyLocalizedFont = true;
        private TMP_Text target;

        public string Key
        {
            get => key;
            set { key = value; Refresh(); }
        }

        private void Awake() => target = GetComponent<TMP_Text>();
        private void OnEnable()
        {
            LocalizationService.Instance.LocaleChanged += OnLocaleChanged;
            Refresh();
        }
        private void OnDisable() => LocalizationService.Instance.LocaleChanged -= OnLocaleChanged;
        private void OnLocaleChanged(string _) => Refresh();
        public void Refresh()
        {
            if (target == null) target = GetComponent<TMP_Text>();
            if (target == null) return;
            if (!string.IsNullOrWhiteSpace(key)) target.text = L10n.Get(key);
            if (applyLocalizedFont && LocalizationService.Instance.ActiveFont != null)
                target.font = LocalizationService.Instance.ActiveFont;
        }
    }
}
