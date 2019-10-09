using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PoolElement: MonoBehaviour, IResettable
{
    [SerializeField] bool isPrefab = false;
    [SerializeField] GameObject _prefab;
    [SerializeField] public int MaxEntities=10;
    [SerializeField] public int id = 0;
    public GameObject Prefab
    {
        get { return _prefab; }
    }

    GameObject instance;
    public float lastResetTime;
    IResettable[] resettableElements;

    private void OnValidate()
    {
        if (resettableElements==null)
        {
            resettableElements = gameObject.GetComponentsInChildren<IResettable>();
        }
		if(isPrefab)
		{
			_prefab = this.gameObject;
		}
    }
    bool resetting = false;
    public void Reset()
    {
        if (!resetting)
        {
            resetting = true;

            OnValidate();

            lastResetTime = Time.time;
            foreach (IResettable resetable in resettableElements)
            {
                resetable.Reset();
            }
            this.name = "[" + id+"]-" + Time.time;

            resetting = false;
        }
    }
}
