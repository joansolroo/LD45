using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizationExample : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(prefab.GetInstanceID());
    }

    // Update is called once per frame
    void Update()
    {
        if(prefab.IsPoolable())
        {
            GameObject instance = gameObject.InstantiateManaged(prefab);
            instance.transform.parent = this.transform;
        }
    }
}
