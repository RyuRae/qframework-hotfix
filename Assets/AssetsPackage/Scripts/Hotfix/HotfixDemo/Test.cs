using QFramework;
using UnityEngine;
using cfg;
using HotfixDemo;

public class Test : MonoBehaviour
{

    GameObject go = null;
    private YooAssetLease<GameObject> mCubeLease;
    private bool mDestroyed;
    void Start()
    {
        

        YooAssetKit.LoadAssetLeaseAsync<GameObject>("Cube", lease =>
        {
            if (mDestroyed)
            {
                lease?.Dispose();
                return;
            }

            mCubeLease = lease;
            var obj = lease == null ? null : lease.Asset;
            if (obj == null)
            {
                LogKit.E("Load Cube failed.");
                return;
            }
            go = Instantiate(obj);
            LogKit.I(go.name);
        });

        // 配置已在 ProcedurePreloadHotfixResources 阶段完成加载和解析。
        TbPerson person = GameConfig.Tables.TbPerson;
        var item = person.DataList[1];
        Person person1 = person.Get(item.Name);
        UnityEngine.Debug.LogFormat("item[1]:{0}", item);
        LogKit.I(person1);

    }
    private void OnDestroy()
    {
        mDestroyed = true;
        mCubeLease?.Dispose();
        mCubeLease = null;
    }

    //private static async UniTask<ByteBuf> LoadByteBuf(string file)
    //{
    //    return new ByteBuf(File.ReadAllBytes($"{Application.dataPath}/../../GenerateDatas/bytes/{file}.bytes"));
    //}

    private void Update()
    {
        if (go == null) return;
        go.transform.Rotate(Vector3.up, 100 * Time.deltaTime);
    }


}
