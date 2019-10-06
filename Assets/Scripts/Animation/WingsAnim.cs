using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsAnim : MonoBehaviour
{
    [SerializeField] public Rigidbody owner;
    [SerializeField] public Transform pivot1;
    [SerializeField] public Transform pivot2;
    [SerializeField] public Transform pivot3;
    [SerializeField] public Transform pivot4;

    [SerializeField] [Range(0, 10)] public float speed;
    [SerializeField] [Range(0, 1)] public float amplitude;

    public float v;
    
    void Update()
    {
        v = owner.velocity.magnitude * amplitude;
        pivot1.localEulerAngles = Vector3.MoveTowards(pivot1.localEulerAngles, new Vector3(0, 0,  owner.velocity.magnitude * amplitude), speed * Time.deltaTime);
        pivot1.localEulerAngles = Vector3.MoveTowards(pivot2.localEulerAngles, new Vector3(0, 0, -owner.velocity.magnitude * amplitude), speed * Time.deltaTime);
        pivot1.localEulerAngles = Vector3.MoveTowards(pivot3.localEulerAngles, new Vector3(0, 0,  owner.velocity.magnitude * amplitude), speed * Time.deltaTime);
        pivot1.localEulerAngles = Vector3.MoveTowards(pivot4.localEulerAngles, new Vector3(0, 0, -owner.velocity.magnitude * amplitude), speed * Time.deltaTime);
    }
}
