using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YooAsset;

namespace Framework.Localization.Components
{
    /// <summary>按本地化资源表地址加载 Sprite，并管理 YooAsset Handle 生命周期。</summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    public sealed class LocalizedImage : MonoBehaviour
    {
        [SerializeField] private string key;
        [SerializeField] private bool setNativeSize;
        private Image target;
        private AssetHandle activeHandle;
        private Coroutine refreshCoroutine;
        private int requestVersion;

        private void Awake() => target = GetComponent<Image>();
        private void OnEnable()
        {
            LocalizationService.Instance.LocaleChanged += OnLocaleChanged;
            RequestRefresh();
        }
        private void OnDisable()
        {
            LocalizationService.Instance.LocaleChanged -= OnLocaleChanged;
            requestVersion++;
            if (refreshCoroutine != null) StopCoroutine(refreshCoroutine);
            refreshCoroutine = null;
            ReleaseActiveHandle();
        }
        private void OnLocaleChanged(string _) => RequestRefresh();
        private void RequestRefresh()
        {
            if (!isActiveAndEnabled) return;
            if (refreshCoroutine != null) StopCoroutine(refreshCoroutine);
            refreshCoroutine = StartCoroutine(Refresh(++requestVersion));
        }
        private IEnumerator Refresh(int version)
        {
            var service = LocalizationService.Instance;
            if (service.RuntimePackage == null || !service.TryGetLocalizedAssetAddress(key, "Sprite", out var address)) yield break;
            AssetHandle nextHandle = service.RuntimePackage.LoadAssetAsync<Sprite>(address);
            yield return nextHandle;
            if (version != requestVersion || nextHandle.Status != EOperationStatus.Succeed)
            {
                nextHandle.Release();
                yield break;
            }
            var oldHandle = activeHandle;
            activeHandle = nextHandle;
            if (target == null) target = GetComponent<Image>();
            target.sprite = nextHandle.AssetObject as Sprite;
            if (setNativeSize && target.sprite != null) target.SetNativeSize();
            oldHandle?.Release();
            refreshCoroutine = null;
        }
        private void ReleaseActiveHandle()
        {
            activeHandle?.Release();
            activeHandle = null;
        }
    }
}
