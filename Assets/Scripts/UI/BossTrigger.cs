using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    [Range(1, 30)] public float detectionRange;
    public Transform player;
    public bool trigger = false;

    // Update is called once per frame
    void Update()
    {
        trigger = (player.position - transform.position).magnitude <= detectionRange;
    }
}
