using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IDamageable
{
    [Header("links")]
    public CharacterController characterController;
    public Equipement equipement;
    [Header("Status")]
    public int hp;
    public int maxHp;
    public int energy;
    public int maxEnergy;

    [Header("Movement")]
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        hp = maxHp;
        energy = maxEnergy;
    }
    
    void Update()
    {
        // movement
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;
        characterController.Move(moveDirection * Time.deltaTime);
    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void AimAt(Vector3 position)
    {
        position.y = this.transform.position.y;
        this.transform.LookAt(position);
    }

    public void Fire1()
    {
        if (equipement.weapon != null)
        {
            equipement.weapon.Fire();
        }
    }

    public void Fire2()
    { }

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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(picked.toPick.GetType() == typeof(Weapon))
            {
                equipement.SetWeapon(((Weapon)picked.toPick).type);
            }
            return true;
        }
        return false;
    }
}
