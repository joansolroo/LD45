using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IDamageable, IPerceptible
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
    public bool invincible = false;
    public int hp;
    public int energy;
    [SerializeField] Vector3 moveDirection = Vector3.zero;
    [SerializeField] Vector3 aimDirection = Vector3.zero;
    public PickableObject hoveredObject = null;
    public bool grounded;
    public bool alive = true;

    void Start()
    {
        equipement.SetWeapon(Weapon.Type.None);
        characterController = GetComponent<CharacterController>();
        Reset();
    }
    
    void Update()
    {
        grounded = characterController.isGrounded;
        // movement
        if (speed >0 ||!grounded)
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
            if (!grounded)
            {
                moveDirection.y -= gravity * Time.deltaTime;
            }
            else
            {
                moveDirection.y = 0;
            }
            characterController.Move(moveDirection * Time.deltaTime);

            if (aimDirection != Vector3.zero)
            {
                turret.transform.LookAt(this.transform.position+ aimDirection);
            }
        }
        if (hoveredObject && Input.GetKeyDown(KeyCode.Space))
        {
            if (hoveredObject.toPick.GetType() == typeof(Weapon))
            {
                equipement.SetWeapon(((Weapon)hoveredObject.toPick).type);
            }else if (hoveredObject.toPick.GetType() == typeof(SecondaryWeapon))
            {
                equipement.SetSecondaryWeapon(((SecondaryWeapon)hoveredObject.toPick).type);
            }

            hoveredObject.GotPicked();
        }
    }

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void AimAt(Vector3 position)
    {
        aimDirection = position-this.transform.position;
        aimDirection.y = 0;
    }

    public void Fire1()
    {
        if (equipement.weapon != null)
        {
            Debug.Log("Fire 1");
            equipement.weapon.Fire();
        }
    }

    public void Fire2()
    {
        if (equipement.secondaryWeapon != null)
        {
            Debug.Log("Fire 2");
            equipement.secondaryWeapon.Fire();
        }
    }

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
        alive = false;
        //this.gameObject.SetActive(false);
    }
    public void Push(Vector3 force)
    {
        Debug.Log("push");
    }
    public void Reset()
    {
        hp = maxHp;
        energy = maxEnergy;
        alive = true;
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
            
            return false;
        }
    }
}
