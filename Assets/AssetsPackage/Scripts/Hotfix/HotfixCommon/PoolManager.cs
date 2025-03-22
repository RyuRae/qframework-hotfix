using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

namespace Common
{
    /// <summary>
    /// ����ع�����
    /// </summary>
    public class PoolManager : Singleton<PoolManager>
    {
        private Dictionary<string, SimpleObjectPool<GameObject>> poolDict = new Dictionary<string, SimpleObjectPool<GameObject>>();

        private SimpleObjectPool<GameObject> pool;

        private PoolManager()
        {

        }

        /// <summary>
        /// ��ʼ����ǰ��������
        /// </summary>
        /// <param name="assetName"></param>
        public void InitPool(string assetName)
        {
            YooAssetKit.LoadAssetAsync<GameObject>(assetName, asset =>
            {
                pool = new SimpleObjectPool<GameObject>(
                    () =>
                    {
                        var clone = GameObject.Instantiate(asset);
                        clone.name = assetName;
                        return clone;
                    },
                    obj =>
                    {
                        obj.SetActive(false);
                    }
                );
                if (!poolDict.ContainsKey(assetName))
                    poolDict.Add(assetName, pool);
            });
        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public GameObject Allocate(string assetName)
        {
            if (poolDict.ContainsKey(assetName))
            {
                pool = poolDict[assetName];
                return pool.Allocate();
            }
            LogKit.W("�����δ��ʼ�������ȳ�ʼ��");
            return null;
        }

        /// <summary>
        /// ���ն�������
        /// </summary>
        /// <param name="obj"></param>
        public void Recycle(GameObject obj)
        {
            string tempName = obj.name;
            if (!poolDict.ContainsKey(tempName))
            {
                LogKit.W("�Ƕ��������");
                return;
            }
            poolDict[tempName].Recycle(obj);

        }

        /// <summary>
        /// �����ͷ���Դ
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
            var it = poolDict.GetEnumerator();
            while (it.MoveNext())
            {
                it.Current.Value.Clear();
            }
            poolDict.Clear();
        }
    }
}
