﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Plane m_Plane;
    private Vector3 hitPoint;

    public float lookSpeed = 5;
    public Controller controller;

    private Vector3 moveDirection = Vector3.zero;

    public static PlayerController main;

    private void Awake()
    {
        main = this;
    }
    void Start()
    {
        m_Plane = new Plane(Vector3.up, Vector3.zero);
        hitPoint = transform.position + transform.forward;

        controller.OnDamage+=OnDamage;
    }

    // Update is called once per frame
    void Update()
    {   // movement
        if (controller.characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            //if (moveDirection.sqrMagnitude > 1) moveDirection.Normalize();
            controller.MoveSubjetive(moveDirection);
        }
        else
        {
            controller.MoveSubjetive(Vector3.zero);
        }
        Vector3 cursorPosition = new Vector3(0.5f, 0.5f, 10);
        Ray ray = Camera.main.ViewportPointToRay(cursorPosition);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        {
            hitPoint = Camera.main.ViewportToWorldPoint(cursorPosition + Vector3.forward * 10);
            controller.AimAt(hitPoint);

            
           /* hitPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * 5);
            controller.AimAt(hitPoint);
            cursor.targetPosition = hitPoint;*/
            //cursor.orientation = hit.normal;


            //camera rotate
            /*Vector3 uvz = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            float deadzone = 0.0f;
            if (deadzone == 0 || uvz.x < (0.5f - deadzone) || uvz.x > (0.5f + deadzone))
            {
                controller.AimRotate(new Vector3(0, (uvz.x - 0.5f) * lookSpeed, 0));
            }*/
            

            // fire
            if (Input.GetMouseButton(0))
            {
                controller.Fire1();
            }
            if (Input.GetMouseButtonDown(1))
            {
                controller.Fire2();
            }
        }
       

    }

    public void OnDamage(int dmg)
    {
        CameraEffects.main.Flash(0.25f, Mathf.Min(0.3f, dmg / 2f));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 delta = new Vector3(0, 0.5f, 0);
        Gizmos.DrawLine(transform.position + delta, hitPoint + delta);
    }
}
