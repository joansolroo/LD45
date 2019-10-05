using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour {

    [SerializeField] PickableObject pickPistol;
    [SerializeField] PickableObject pickShotgun;
    [SerializeField] PickableObject pickRifle;
    [SerializeField] PickableObject pickBooze;

    static DropManager instance;
    void Awake()
    {
        instance = this;
    }
    public static PickableObject DropAt(PickableObject.PickableType type, Vector3 pos)
    {
        PickableObject template = null;
        if(type == PickableObject.PickableType.booze)
        {
            template = instance.pickBooze;
        }
        else if (type == PickableObject.PickableType.pistol)
        {
            template = instance.pickPistol;
        }
        else if (type == PickableObject.PickableType.shotgun)
        {
            template = instance.pickShotgun;
        }
        else if (type == PickableObject.PickableType.rifle)
        {
            template = instance.pickRifle;
        }

        if (template != null)
        {
            PickableObject result = GameObject.Instantiate<PickableObject>(template);
            result.gameObject.SetActive(true);
            result.transform.position = pos;
            result.transform.parent = template.transform.parent;
            result.transform.localScale = template.transform.localScale;

            return result;
        }
        else
        {
            return null;
        }
    }
    
}
