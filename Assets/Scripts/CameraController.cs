using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset;

    private void Start()
    {
        //offset = this.transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        this.transform.position = target.transform.position + offset;
    }
}
