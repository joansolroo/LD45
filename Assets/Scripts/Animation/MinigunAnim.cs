using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigunAnim : MonoBehaviour
{
    [SerializeField] public Weapon weapon;
    [SerializeField] public Transform pivot1;
    [SerializeField] public Transform pivot2;
    [SerializeField] [Range(500, 1000)] public float speed;
    
    void Update()
    {
        if(weapon.firing)
        {
            pivot1.localEulerAngles += new Vector3(0, 0,  speed * Time.deltaTime);
            pivot2.localEulerAngles += new Vector3(0, 0, -speed * Time.deltaTime);
        }
    }
}
