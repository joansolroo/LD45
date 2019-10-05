using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IDamageable
{
    CharacterController characterController;

    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private Vector3 moveDirection = Vector3.zero;
    private Plane m_Plane;
    private Vector3 hitPoint;

    public Weapon weapon;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        m_Plane = new Plane(Vector3.up, Vector3.zero);
        hitPoint = transform.position + transform.forward;
    }
    
    void Update()
    {
        // movement
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
        transform.LookAt(hitPoint);

        // fire
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float enter = 0.0f;

            if (m_Plane.Raycast(ray, out enter))
            {
                hitPoint = ray.GetPoint(enter);
            }
            weapon.Fire();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 delta = new Vector3(0,0.5f,0);
        Gizmos.DrawLine(transform.position + delta, hitPoint + delta);
    }

    public void Damage(int amount)
    {
        Debug.Log("Damaged");
    }
    public void Push(Vector3 force)
    {
        Debug.Log("push");
    }

    public bool OnPickObject(PickableObject picked)
    {
        Debug.Log("pick "+ picked.name);
        return true;
    }
}
