﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour,IDamageable {
    
    [SerializeField] int hp;
    [SerializeField] PickableObject drop;

    void Start()
    {
        
    }
    void LateUpdate()
    {
        triggered = false;
    }
    bool triggered = false;
    private void OnCollisionEnter(Collision collision)
    {
        
        //Debug.Log("hitted by " + collision.collider.name+"::"+ collision.gameObject.layer);
        if (collision.gameObject.layer == 10 && collision.gameObject.tag != this.tag)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            if (bullet != null)
            {
                if (!triggered)
                {
                    triggered = true;
                    Damage(bullet.damage);
                }
            }
        }
    }
    bool Damage(int amount)
    {

        if (!triggered)
        {
            triggered = true;
            hp -= amount;
            if (hp > 0)
            {
                
            }
            else
            {
                Destroy();
            }
            return true;
        }
        return false;
    }
    bool IDamageable.Damage(int amount)
    {
        return Damage(amount);
    }

    void Destroy()
    {
        if (drop != null)
        {
            PickableObject newDrop = GameObject.Instantiate<PickableObject>(drop);
            newDrop.transform.position = this.transform.position;
            newDrop.gameObject.SetActive(true);
            newDrop.transform.parent = drop.transform.parent;
            newDrop.transform.localScale = drop.transform.localScale;
        }
        EffectManager.main.Explode(this.transform.position);
        Destroy(this.gameObject);
    }
}
