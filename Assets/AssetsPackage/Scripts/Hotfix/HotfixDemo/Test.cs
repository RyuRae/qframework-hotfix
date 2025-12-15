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
    async void Start()
    {
        

        YooAssetKit.LoadAssetAsync<GameObject>("Cube", obj => 
        {
            go = Instantiate(obj);
            LogKit.I(go.name);
        });

        byte[] datas = await LoadTable();

        TbPerson person = new TbPerson(new ByteBuf(datas));
        var item = person.DataList[1];
        Person person1 = person.Get("Ð¡½ð");
        UnityEngine.Debug.LogFormat("item[1]:{0}", item);
        LogKit.I(person1);

    }


    private async UniTask<byte[]> LoadTable()
    {
        var textAsset = await YooAssetKit.LoadAssetAsync<TextAsset>("tbperson");
        return textAsset.bytes;
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
