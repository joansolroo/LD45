﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    public Rigidbody rb;
    public int velocity = 1;
    public int damage = 1;
    public float ttl = 10;
    public LayerMask target;
    public void Start()
    {
        ttl *= Random.Range(0.95f, 1.05f);
    }
    private void LateUpdate()
    {
        ttl -= Time.deltaTime;
        if(ttl <= 0)
        {
            DestroyBullet();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(target.Contains(collision.gameObject.layer))
        {
            IDamageable damageable =  collision.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                damageable.Damage(damage);
            }
            DestroyBullet();
        }
        
    }

    void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
}