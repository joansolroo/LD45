using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedPrefab : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    GameObject instance;
    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            if(transform.childCount!=0)
            {
                for(int c = 0; c < transform.childCount; ++c)
                {
                    this.DestroyEndofTurn(transform.GetChild(c).gameObject);
                }
                instance = null;
            }
            if (instance == null)
            {
                Create();
                instance.transform.hideFlags = HideFlags.NotEditable;
                instance.transform.hideFlags &= HideFlags.DontSaveInEditor;
            }
        }
    }
    private void Start()
    {
        Create();

    }

    void Create()
    {
        instance = gameObject.InstantiateManaged(prefab);
        instance.name = prefab.name;
        instance.transform.parent = this.transform;
        this.name = prefab.name + " [wrap]";
    }
    private void OnDestroy()
    {
        instance.DestroyManaged();
    }
}
