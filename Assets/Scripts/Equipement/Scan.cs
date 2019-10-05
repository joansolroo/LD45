using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scan : MonoBehaviour
{
    public enum Type
    {
        Radar,
        Radio
    };

    // Attributes
    [Header("Scan")]
    [SerializeField] public Type type;
    [SerializeField] public int alignment;
}
