using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : MonoBehaviour
{
    public enum Type
    {
        HUD,
        FuturHUD
    }

    // Attributes
    [Header("Passive")]
    [SerializeField] public Type type;
    [SerializeField] public int alignment;
}
