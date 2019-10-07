using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalAnim : MonoBehaviour
{
    [SerializeField] public Controller owner;
    [SerializeField] public Transform pivot;
    [SerializeField] [Range(0, 100)] public float speed;
    [SerializeField] [Range(0, 60)] public float amplitude;

    void Update()
    {
        if (owner != null && pivot != null)
            pivot.localEulerAngles = Vector3.MoveTowards(pivot.localEulerAngles, new Vector3(owner.moving ? amplitude : 0, 0, 0), speed * Time.deltaTime);
    }
}
