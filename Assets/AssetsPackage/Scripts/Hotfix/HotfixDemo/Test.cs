using QFramework;
using System.Collections;
using UnityEngine;
using YooAsset;
using Luban;
using Cysharp.Threading.Tasks;
using cfg;

public class Test : MonoBehaviour
{

    GameObject go = null;
    private YooAssetLease<GameObject> mCubeLease;
    private bool mDestroyed;
    async void Start()
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

        byte[] datas = await LoadTable();

        TbPerson person = new TbPerson(new ByteBuf(datas));
        var item = person.DataList[1];
        Person person1 = person.Get(item.Name);
        UnityEngine.Debug.LogFormat("item[1]:{0}", item);
        LogKit.I(person1);

    }


    private async UniTask<byte[]> LoadTable()
    {
        using (var lease = await YooAssetKit.LoadAssetLeaseAsync<TextAsset>("tbperson"))
        {
            return lease.Asset.bytes;
        }
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
