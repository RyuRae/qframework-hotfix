using QFramework;
using System.Collections;
using UnityEngine;
using YooAsset;

public class Test : MonoBehaviour
{

    GameObject go = null;
    IEnumerator Start()
    {
        Debug.Log("Hello, ×Ô¶¯Ðý×ª²âÊÔ£¡");
        var package = YooAssets.GetPackage("DefaultPackage");
        var handle = package.LoadAssetAsync<GameObject>("Cube");
        yield return handle;
        go = handle.InstantiateSync();
        LogKit.I(go.name);
    }

    private void Update()
    {
        if (go == null) return;
        go.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }


}
