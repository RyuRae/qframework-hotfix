using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;
using System;

namespace Common
{
    /// <summary>
    /// 对象池管理器
    /// </summary>
    public class PoolManager : Singleton<PoolManager>
    {
        private Dictionary<string, SimpleObjectPool<GameObject>> poolDict = new Dictionary<string, SimpleObjectPool<GameObject>>();

        private SimpleObjectPool<GameObject> pool;

        private PoolManager()
        {

        }

        /// <summary>
        /// 初始化当前物体对象池
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
        /// 获取对象物体
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
            LogKit.W("对象池未初始化，请先初始化");
            return null;
        }

        /// <summary>
        /// 回收对象物体
        /// </summary>
        /// <param name="obj"></param>
        public void Recycle(GameObject obj)
        {
            string tempName = obj.name;
            if (!poolDict.ContainsKey(tempName))
            {
                LogKit.W("非对象池物体");
                return;
            }
            poolDict[tempName].Recycle(obj);

        }

        /// <summary>
        /// 清理释放资源
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
