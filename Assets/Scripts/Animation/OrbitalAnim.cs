using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalAnim : MonoBehaviour
{
    [SerializeField] public Rigidbody owner;
    [SerializeField] public Transform pivot;
    [SerializeField] [Range(0, 10)] public float speed;
    [SerializeField] [Range(0, 1)] public float amplitude;

    void Update()
    {
        pivot.localEulerAngles = Vector3.MoveTowards(pivot.localEulerAngles, new Vector3(owner.velocity.magnitude * amplitude, 0, 0), speed * Time.deltaTime);
    }
}
