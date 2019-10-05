using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    public enum Type
    {
        Rockets,
        Asteroid,
        GuidedRocket,
        OrbitalLaser
    };

    // Attributes
    [Header("SecondaryWeapon")]
    [SerializeField] public Type type;
}
