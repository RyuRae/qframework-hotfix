using QFramework;
using System.Collections;
using UnityEngine;
using YooAsset;

public class Test : MonoBehaviour
{

    GameObject go = null;
    void Start()
    {
        //Debug.Log("Hello, ×Ô¶¯Ðý×ª²âÊÔ£¡");
        //var package = YooAssets.GetPackage("DefaultPackage");
        //var handle = package.LoadAssetAsync<GameObject>("Cube");
        //yield return handle;
        //go = handle.InstantiateSync();
        //LogKit.I(go.name);

        YooAssetKit.LoadAssetAsync<GameObject>("Cube", obj => 
        {
            go = Instantiate(obj);
            LogKit.I(go.name);
        });
    }

    private void Update()
    {
        if (go == null) return;
        go.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }


}
