using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtension
{
    public static GameObject InstantiateManaged(this GameObject go, GameObject reference)
    {
        Debug.Log(reference.name + " poolable? " + go.IsPoolable());
        if (reference.IsPoolable())
        {
            return PoolManager.Get(reference).gameObject;
        }
        else
        {
            return GameObject.Instantiate(reference);
        }
    }

    public static T InstantiateManaged<T>(this GameObject go, T reference) where T : Component
    {
        GameObject referenceGO = reference.gameObject;
        Debug.Log(go.name + " poolable? " + referenceGO.IsPoolable());
        if (referenceGO.IsPoolable())
        {
            return PoolManager.Get(referenceGO).gameObject.GetComponent<T>();
        }
        else
        {
            return GameObject.Instantiate<T>(reference);
        }
        
    }

    public static bool IsPoolable(this GameObject go)
    {
        return PoolManager.IsPoolable(go);
    }

    public static void DestroyManaged(this GameObject go)
    {
        if (go.IsPoolable())
        {
            PoolManager.Release(go);
        }
        else
        {
            GameObject.Destroy(go);
        }
    }
}
