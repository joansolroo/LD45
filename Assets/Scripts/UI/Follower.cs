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

    [SerializeField] Cursor cursor;
    [SerializeField] LayerMask pointLayer;
    [SerializeField] Vector3 cursorPosition = new Vector3(0.5f, 0.5f, 10);
    [SerializeField] Vector2 aimVelocity = new Vector2(60,0.1f);

    void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked; // keep confined in the game window
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None; // keep confined in the game window
        }
    }
    private void Update()
    {
        LateUpdate();
    }
    void LateUpdate()
    {
        transform.position = target.position;
        /*if (targetOrientation)
        {
            float currentAngle = Quaternion.Angle(transform.rotation, targetOrientation.rotation);
            if (currentAngle > rotationDeadZone)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetOrientation.rotation, Mathf.Pow(Mathf.Clamp01(((currentAngle - rotationDeadZone) / (rotationMax - rotationDeadZone))), 2) * (rotationSpeed) * Time.deltaTime);
            }
        }*/
        this.transform.Rotate(0, Input.GetAxis("Mouse X")* aimVelocity.x*Time.deltaTime, 0);
        cursorPosition.y += Input.GetAxis("Mouse Y") * aimVelocity.y * Time.deltaTime;
        cursorPosition.y = Mathf.Clamp01(cursorPosition.y);

        cursor.centerTransform.position = this.transform.position;
        cursor.centerTransform.rotation = this.transform.rotation;

        cursor.localAim.localPosition = new Vector3(0,cursorPosition.y-0.5f,0);
        cursor.localAim.rotation = this.transform.rotation;
        //Ray ray = Camera.main.ViewportPointToRay(cursorPosition);
        Ray ray = new Ray(Camera.main.transform.position, cursor.localAim.position - Camera.main.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, pointLayer))
        {
            if (hit.point.y > 0.5f)
            {
                cursor.targetPosition = hit.point;
                cursor.targetPosition.y = 0.5f;
            }
            else
            {
                cursor.targetPosition = hit.point;
            }

            cursor.orientation = hit.normal;
        }
        cursor.worldAim.position = cursor.targetPosition;
        cursor.worldAim.forward = cursor.orientation;
        cursor.aoeAim.position = cursor.targetPosition;
        cursor.aoeAim.forward = cursor.orientation;


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.ViewportToWorldPoint(cursorPosition));
    }
}
