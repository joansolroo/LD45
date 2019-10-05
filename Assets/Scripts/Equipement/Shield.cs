using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public enum Type
    {
        Standard,
        SteelPlate,
        Magnetic
    };

    // Attributes
    [Header("Shield")]
    [SerializeField] public Type type;
}
