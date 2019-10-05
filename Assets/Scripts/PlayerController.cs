using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Plane m_Plane;
    private Vector3 hitPoint;

    public Controller controller;

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        m_Plane = new Plane(Vector3.up, Vector3.zero);
        hitPoint = transform.position + transform.forward;
    }

    // Update is called once per frame
    void Update()
    {   // movement
        if (controller.characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            //if (moveDirection.sqrMagnitude > 1) moveDirection.Normalize();
            controller.Move(moveDirection);
        }
        else
        {
            controller.Move(Vector3.zero);
        }
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        {
            controller.AimAt(hitPoint);

            float enter = 0.0f;

            if (m_Plane.Raycast(ray, out enter))
            {
                hitPoint = ray.GetPoint(enter);
            }
            // fire
            if (Input.GetMouseButton(0))
            {
                controller.Fire1();
            }
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 delta = new Vector3(0, 0.5f, 0);
        Gizmos.DrawLine(transform.position + delta, hitPoint + delta);
    }
}
