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
    [SerializeField] public string passiveName = "unknown";
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


/*
green 255 194 71  ;
orange 99 255 88  ;
*/
