using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public GameObject prefab;
    public int maxInstance = 5;

    private List<GameObject> pool = new List<GameObject>();
    public int lastInstanceIndex;

    void Start()
    {
        lastInstanceIndex = 0;
    }
    
    public GameObject Get()
    {
        if(pool.Count < maxInstance)
        {
            pool.Add(Instantiate(prefab));
            lastInstanceIndex = pool.Count - 1;
            return pool[lastInstanceIndex];
        }
        else
        {
            GameObject go = pool[lastInstanceIndex];
            lastInstanceIndex = (lastInstanceIndex + 1) % maxInstance;
            return go;
        }
    }
}
