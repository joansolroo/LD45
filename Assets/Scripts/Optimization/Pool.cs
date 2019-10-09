using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    public class Pool : Dictionary<GameObject, List<PoolElement>> { };
    public static Pool pool = new Pool();
    static Dictionary<GameObject, PoolElement> instanceList;
    public static Pool used = new Pool();
    public static Pool available = new Pool();


    public static bool IsPoolable(GameObject prefab)
    {
        return prefab.GetComponent<PoolElement>() != null;
    }
    public static PoolElement GetNew(GameObject prefab)
    {
        PoolElement template = prefab.GetComponent<PoolElement>();
        PoolElement poolElement = null;
        if (!pool.ContainsKey(prefab))
        {
            List<PoolElement> list = new List<PoolElement>();
            pool[prefab] = list;
            List<PoolElement> listAvailable = new List<PoolElement>();
            available[prefab] = listAvailable;
            List<PoolElement> listUsed = new List<PoolElement>();
            used[prefab] = listUsed;
        }
        {
            if (pool[prefab].Count < template.MaxEntities)
            {
                poolElement = GameObject.Instantiate<PoolElement>(template);
                
                pool[prefab].Add(poolElement);
                poolElement.id = pool[prefab].Count;
                used[prefab].Add(poolElement);
            }
        }
        return poolElement;
    }
    public static PoolElement GetAvailable(GameObject prefab)
    {
        if (available.ContainsKey(prefab))
        {
            List<PoolElement> listAvailable = available[prefab];
            if (listAvailable.Count > 0)
            {
                PoolElement poolElement = listAvailable[0];
                listAvailable.RemoveAt(0);
                List<PoolElement> listUsed;
                if (!used.ContainsKey(prefab))
                {
                    listUsed = new List<PoolElement>();
                    used[prefab] = listUsed;
                }
                else
                {
                    listUsed = used[prefab];
                }
                listUsed.Add(poolElement);
                poolElement.Reset();
                return poolElement;
            }
        }
        return null;
    }
    public static PoolElement GetUsed(GameObject prefab)
    {
        if (available.ContainsKey(prefab))
        {
            List<PoolElement> listUsed = used[prefab];
            if (listUsed.Count > 0)
            {
                PoolElement poolElement = listUsed[0];
                listUsed.RemoveAt(0);
                listUsed.Add(poolElement);
                poolElement.Reset();
                return poolElement;
            }
        }
        return null;
    }
    public static PoolElement Get(GameObject prefab)
    {
        PoolElement poolElement = GetNew(prefab);
        if (poolElement == null)
        {
            poolElement = GetAvailable(prefab);
        }
        
        if (poolElement == null)
        {
            poolElement = GetUsed(prefab);
        }
        return poolElement;
    }
    public static void Release(GameObject instance)
    {
        try
        {
            instance.SetActive(false);
            PoolElement poolElement = instanceList[instance];
            used[poolElement.Prefab].Remove(poolElement);
            available[poolElement.Prefab].Add(poolElement);
        }
        catch (System.Exception e)
        {
            Debug.LogError("POOLING ERROR: " + instance + " not an instance");
        }
    }
}
