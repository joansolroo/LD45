﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IDamageable
{
    [Header("links")]
    public CharacterController characterController;
    public Equipement equipement;
    public Transform turret;

    [Header("Status")]
    public int maxHp;
    public int maxEnergy;

    [Header("Movement")]
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    [Header("Debug values")]
    public int hp;
    public int energy;
    [SerializeField] Vector3 moveDirection = Vector3.zero;
    [SerializeField] Vector3 aimDirection = Vector3.zero;
    public Component hoveredObject = null;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        hp = maxHp;
        energy = maxEnergy;
    }
    
    void Update()
    {
        // movement
        if (speed >0 ||!characterController.isGrounded)
        {
            
            if (moveDirection.sqrMagnitude > 0.1f)
            {
                this.transform.forward = Vector3.MoveTowards(this.transform.forward,moveDirection.normalized,Time.deltaTime*360);
            }
            /*else
            {
                this.transform.LookAt(this.transform.position + this.transform.forward);
            }*/
            moveDirection *= speed;
            moveDirection.y -= gravity * Time.deltaTime;
            characterController.Move(moveDirection * Time.deltaTime);

            if (aimDirection != Vector3.zero)
            {
                turret.transform.LookAt(this.transform.position+ aimDirection);
            }
        }
    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void AimAt(Vector3 position)
    {
        position.y = this.transform.position.y;
        aimDirection = position-this.transform.position;
    }

    public void Fire1()
    {
        if (equipement.weapon != null)
        {
            Debug.Log("fire");
            equipement.weapon.Fire();
        }
    }

    public void Fire2()
    { }

    public void Damage(int amount)
    {
        this.hp -= amount;
        if(this.hp<=0)
        {
            this.hp = 0;
            Die();
        }
        Debug.Log("Damaged");
    }

    void Die()
    {
        Debug.Log("die");
        Destroy(this.gameObject);
    }
    public void Push(Vector3 force)
    {
        Debug.Log("push");
    }

    public bool OnPickObject(PickableObject canPick, bool end)
    {
        if (end)
        {
            hoveredObject = null;
            return false;
        }
        else
        {
            hoveredObject = canPick;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (canPick.toPick.GetType() == typeof(Weapon))
                {
                    equipement.SetWeapon(((Weapon)canPick.toPick).type);
                }
                return true;
            }
            return false;
        }
    }
}
