using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ObjectPool
{    
    private GameObject m_Pool;
    private Dictionary<string, Queue<GameObject>> m_ObjectPool = new Dictionary<string, Queue<GameObject>>();
    private Dictionary<string, GameObject> m_Pools = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_Players = new Dictionary<string, GameObject>();
    public void PushObject(GameObject prefab, bool needBackPool = true)
    {
        // Debug.Log(prefab);
        string name = prefab.name.Replace("(Clone)", string.Empty);
        if (!m_ObjectPool.ContainsKey(name))
        {
            m_ObjectPool.Add(name, new Queue<GameObject>());
        }
        m_ObjectPool[name].Enqueue(prefab);
        if(needBackPool) prefab.transform.SetParent(m_Pools[name+"Pool"].transform);
        // Debug.Log(m_ObjectPool[name].Count);
        prefab.SetActive(false);
    }
    public GameObject GetPrefabObject(string objName, string lable)
    {
        GameObject _object;
        // 是否存在预制体的对象池
        if (!m_ObjectPool.ContainsKey(objName) || m_ObjectPool[objName].Count == 0)
        {
            // 获取AssetFactory
            IAssetFactory factory = IFactory.GetAssetFactory();
            if(lable == "Enemys")
                _object = factory.LoadPrefabEnemys(objName);
            else if(lable == "Others")
                _object = factory.LoadPrefabOthers(objName);
            else
                _object = factory.LoadPrefab(objName);
            // 是否存在对象池父物体
            if (m_Pool == null)
            {
                m_Pool = new GameObject("ObjectPool");
            }
            // 是否存在子对象池的父物体
            GameObject childPool;
            string poolName = objName + "Pool";
            if (m_Pools.TryGetValue(poolName, out childPool) == false)
            {
                childPool = new GameObject(poolName);
                childPool.transform.SetParent(m_Pool.transform);
                m_Pools.Add(poolName, childPool);
            }
            // _object.transform.SetParent(childPool.transform);
            PushObject(_object,needBackPool:true);
        }
        _object = m_ObjectPool[objName].Dequeue();
        _object.SetActive(true);
        return _object;
    }
    public GameObject LoadPlayerModle(string objName)
    {
        GameObject _object;
        if(m_Players.ContainsKey(objName) == false)
        {
            _object = GameObject.Instantiate( IFactory.GetAssetFactory().LoadPrefabPlayers(objName), IGameManager.Instance.GetPlayerHandle().transform);
            PushPlayer(_object);
        }
        _object = m_Players[objName];
        _object.SetActive(true);
        Debug.Log(_object.name + "加载");
        return _object;
    }
    public void PushPlayer(GameObject obj)
    {
        string name = obj.name.Replace("(Clone)", string.Empty);
        if(m_Players.ContainsKey(name) == false)
            m_Players.Add(name, obj);
        m_Players[name].SetActive(false);
    }
    public void Release()
    {
        m_ObjectPool.Clear();
        m_Players.Clear();
        m_Pool = null;
        m_Pools.Clear();
    }
}
