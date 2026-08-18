using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 跨场景保留的协程宿主，为非 MonoBehaviour 的 Procedure 状态提供统一协程执行环境。
/// </summary>
public class CoroutineController : MonoBehaviour {
    public static CoroutineController manager = null;

    private void Awake() {
        if (manager == null) {
            DontDestroyOnLoad(gameObject);
            manager = this;
        } else if (manager != this) {
            Destroy(gameObject);
        }
    }
}
