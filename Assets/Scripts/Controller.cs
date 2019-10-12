using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, IDamageable, IPerceptible
{
    public bool isPlayer = false;
    [Header("links")]
    public CharacterController characterController;
    public Equipement equipement;
    public Transform turret;
    public Transform body;

    [Header("Status")]
    public int maxHp;
    public int maxEnergy;
    public bool verticalAim = false;
    [Header("Movement")]
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    [Header("Debug values")]
    public bool invincible = false;
    public bool canPickObj = false;
    public int hp;
    public int energy;
    public int points = 0;
    [SerializeField] Vector3 moveDirection = Vector3.zero;
    [SerializeField] Vector3 aimDirection = Vector3.zero;
    public PickableObject hoveredObject = null;
    public bool grounded;
    public bool alive = true;
    public bool moving;

    public delegate void ControllerEventInt(int num);
    public delegate void ControllerEventt();
    public ControllerEventInt OnDamage;
    
    AudioSource audioSource;
    void Start()
    {
        audioSource = this.gameObject.AddComponent<AudioSource>();
        if (!body)
        {
            body = this.transform;
        }
        characterController = GetComponent<CharacterController>();
        Reset();
    }

    public bool firedLeft = false;
    public bool firedRight = false;
    void Update()
    {
        grounded = characterController.isGrounded;

        // movement
        if (speed > 0 || !grounded)
        {
            if (moveDirection.sqrMagnitude > 0.1f)
            {
                body.transform.rotation = Quaternion.RotateTowards(body.transform.rotation, Quaternion.LookRotation(moveDirection.normalized, Vector3.up), Time.deltaTime * 360 * 2);
            }

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
                turret.transform.LookAt(turret.transform.position + aimDirection);
            }
        }
        if (canPickObj && hoveredObject && Input.GetKeyDown(KeyCode.Space))
        {
            if (hoveredObject.toPick.GetType() == typeof(Weapon))
            {
                equipement.SetWeapon(((Weapon)hoveredObject.toPick).type);
            }
            else if (hoveredObject.toPick.GetType() == typeof(SecondaryWeapon))
            {
                equipement.SetSecondaryWeapon(((SecondaryWeapon)hoveredObject.toPick).type);
            }
            else if (hoveredObject.toPick.GetType() == typeof(Passive))
            {
                equipement.SetPassive(((Passive)hoveredObject.toPick).type);
            }

            hoveredObject.GotPicked();
        }

        float alpha = 0.7f;
        smoothedVelocity = (1.0f - alpha) * smoothedVelocity + alpha * new Vector2(characterController.velocity.x, characterController.velocity.z);
        moving = smoothedVelocity.magnitude > 0.9f;
    }
    private Vector2 smoothedVelocity;

    public void Move(Vector3 direction)
    {
        moveDirection = direction;
    }
    public void MoveSubjetive(Vector3 localDirection)
    {
        moveDirection = turret.TransformDirection(localDirection);
    }
    public void AimAt(Vector3 position)
    {
        aimDirection = position - this.transform.position;
        if(!verticalAim)aimDirection.y = 0;
    }
    public void AimRotate(Vector3 angle)
    {
        aimDirection = Quaternion.Euler(angle) * turret.forward;
        if (!verticalAim) aimDirection.y = 0;
    }

    public void Fire1()
    {
        if (equipement.weapon != null)
        {
            equipement.weapon.Fire();
            firedLeft = true;
        }
    }
    public void Fire2()
    {
        if (equipement.secondaryWeapon != null)
        {
            equipement.secondaryWeapon.Fire();
            firedRight = true;
        }
    }

    public void Damage(int amount)
    {
        hp -= amount;
        OnDamage?.Invoke(amount);
        if (hp <= 0 && !invincible)
        {
            hp = 0;
            Die();
        }
    }
    void Die()
    {
        EffectManager.main.Explode(transform.position+Vector3.up*0.5f);
        alive = false;
        gameObject.SetActive(false);
    }
    
    public void Reset()
    {
        hp = maxHp;
        energy = maxEnergy;
        alive = true;
        if (equipement.weapon != null)
            equipement.weapon.Reset();
        if (equipement.secondaryWeapon != null)
            equipement.secondaryWeapon.Reset();
    }

    public bool OnPickObject(PickableObject canPick, bool end)
    {
        if(!isPlayer)
        {
            return false;
        }
        bool result = false;
        if (!canPick.instant)
        {
            if (end)
            {
                hoveredObject = null;
                result = false;
            }
            else
            {

                hoveredObject = canPick;
                result = false;
            }
        }
        else if (!end)
        {
            PickableRestore restore = (PickableRestore)canPick.toPick;
            if (restore)
            {
                if (restore.pickableType == PickableRestore.Type.Ammo)
                {
                    if (equipement.weapon != null)
                    {
                        if (equipement.weapon.load < equipement.weapon.capacity)
                        {
                            equipement.weapon.load = equipement.weapon.capacity;
                            result = true;
                        }
                    }
                }
                else if (restore.pickableType == PickableRestore.Type.HP)
                {
                    if (hp < maxHp)
                    {
                        hp = maxHp;
                        result = true;
                    }

                }
                else if (equipement.secondaryWeapon != null && restore.pickableType == PickableRestore.Type.Energy)
                {
                    if (equipement.secondaryWeapon.load < equipement.secondaryWeapon.capacity)
                    {
                        equipement.secondaryWeapon.load = equipement.secondaryWeapon.capacity;
                        equipement.secondaryWeapon.overheat = false;
                        result = true;
                    }
                }
                else if (restore.pickableType == PickableRestore.Type.Data)
                {
                    points++;
                    result = true;
                }
                
                if(result)
                {
                    if(restore.audioclip!=null)
                    {
                        this.audioSource.PlayOneShot(restore.audioclip);
                    }
                }
            }
            
        }
        return result;
    }

    private void OnDrawGizmos()
    {
        if (body)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(body.transform.position, body.transform.position + body.transform.forward);
        }
        if (turret)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(turret.transform.position, turret.transform.position + turret.transform.forward);
        }
    }
}
