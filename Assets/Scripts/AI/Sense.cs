using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sense : MonoBehaviour
{
    public LayerMask layer;
    public Dictionary<GameObject, IPerceptible> pereceived = new Dictionary<GameObject, IPerceptible>();
    public int perceivedCount;
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            GameObject go = other.attachedRigidbody.gameObject;
            if (layer.Contains(go.layer))
            {
                Debug.Log("sensed " + other.name);
                pereceived[go] = go.GetComponent<IPerceptible>();
                ++perceivedCount;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject go;
        if (other.attachedRigidbody)
        {
            go = other.attachedRigidbody.gameObject;
        }
        else
        {
            go = other.gameObject;
        }
        
        if (layer.Contains(go.layer))
        {
            pereceived.Remove(go);
            --perceivedCount;
        }
    }
}
