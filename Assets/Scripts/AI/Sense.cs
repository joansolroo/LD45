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
        GameObject go = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
        if (layer.Contains(go.layer))
        {
            Debug.Log("sensed " + other.name);
            pereceived[go] = go.GetComponent<IPerceptible>();
            perceivedCount = pereceived.Count;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        GameObject go = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
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
            perceivedCount = pereceived.Count;
        }
    }

    private void OnDrawGizmos()
    {
        foreach(GameObject p in pereceived.Keys)
        {
            Gizmos.DrawLine(this.transform.position, p.transform.position);
        }
    }
}
