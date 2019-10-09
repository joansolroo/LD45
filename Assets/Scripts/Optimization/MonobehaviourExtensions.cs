using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoBehaviourExtension
{
    public static void DestroyEndofTurn(this MonoBehaviour mono, GameObject reference)
    {
        mono.StartCoroutine(mono.DoDestroyEndofTurn(reference));

    }

    public static IEnumerator DoDestroyEndofTurn(this MonoBehaviour mono, GameObject go)
    {
        yield return new WaitForEndOfFrame();
        GameObject.DestroyImmediate(go);
    }
}
