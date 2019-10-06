using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Follower : MonoBehaviour
{
    public Transform target;
    public Transform targetOrientation;

    public float rotationDeadZone = 1;
    public float rotationMax = 1;
    public float rotationSpeed;

    private void Update()
    {
        LateUpdate();
    }
    void LateUpdate()
    {
        transform.position = target.position;
        if (targetOrientation)
        {
            float currentAngle = Quaternion.Angle(transform.rotation, targetOrientation.rotation);
            if (currentAngle > rotationDeadZone)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetOrientation.rotation, Mathf.Pow(Mathf.Clamp01(((currentAngle - rotationDeadZone) / (rotationMax - rotationDeadZone))), 2) * (rotationSpeed) * Time.deltaTime);
            }
        }
    }
}
