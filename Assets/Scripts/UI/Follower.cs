using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Transform target;
    public Transform targetOrientation;

    public float rotationDeadZone = 1;
    public float rotationSpeed;
    void LateUpdate()
    {
        transform.position = target.position;
        float currentAngle = Quaternion.Angle(transform.rotation, targetOrientation.rotation);
        if (currentAngle > rotationDeadZone)
        {
            transform.rotation =Quaternion.RotateTowards(transform.rotation, targetOrientation.rotation, (rotationSpeed) *Time.deltaTime);
        }
    }
}
