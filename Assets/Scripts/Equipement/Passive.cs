using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : MonoBehaviour
{
    public enum Type
    {
        Radar,
        Turret
    }

    // Attributes
    [Header("Passive")]
    [SerializeField] public Type type;
    [SerializeField] public int alignment;
    [SerializeField] public GameObject iconPrefab;

    public string GetLiteralType()
    {
        switch (type)
        {
            case Type.Radar: return "radar";
            default: return "turret";
        }
    }
}
