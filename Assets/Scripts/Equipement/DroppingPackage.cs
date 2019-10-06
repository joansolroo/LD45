using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingPackage : MonoBehaviour
{  
    void OnCollisionEnter(Collision collision)
    {
        foreach(Transform child in transform)
        {
            child.parent = null;
            child.GetComponent<PickableObject>().Init();
        }
        Destroy(gameObject);
    }
}
