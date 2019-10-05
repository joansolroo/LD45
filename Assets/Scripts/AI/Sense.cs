﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class Sense : MonoBehaviour
{
    public LayerMask layer;
    public Dictionary<GameObject, IPerceptible> perceived = new Dictionary<GameObject, IPerceptible>();
    public int perceivedCount;

    public IEnumerable<TKey> RandomKeys<TKey, TValue>(IDictionary<TKey, TValue> dict)
    {
        List<TKey> keys = Enumerable.ToList(dict.Keys);
        int size = dict.Count;
        while (true)
        {
            yield return keys[Random.Range(0, size)];
        }
    }
    public IEnumerable<TValue> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict)
    {
        List<TValue> values = Enumerable.ToList(dict.Values);
        int size = dict.Count;
        while (true)
        {
            yield return values[Random.Range(0,size)];
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
        if (layer.Contains(go.layer))
        {
            Debug.Log("sensed " + other.name);
            perceived[go] = go.GetComponent<IPerceptible>();
            perceivedCount = perceived.Count;
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
            perceived.Remove(go);
            perceivedCount = perceived.Count;
        }
    }
    Random rand = new Random();
    public GameObject GetRandom()
    {
        return RandomKeys(perceived).First();
    }
    public bool IsVisible(GameObject go)
    {
        return perceived.ContainsKey(go);
    }

    private void OnDrawGizmos()
    {
        foreach (GameObject p in perceived.Keys)
        {
            Gizmos.DrawLine(this.transform.position, p.transform.position);
        }
    }
}
