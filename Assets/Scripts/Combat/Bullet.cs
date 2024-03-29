﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public bool pooled = true;
    public TrailRenderer trail;
    public GameObject mesh;
    public Rigidbody rb;

    public int velocity = 1;
    public int damage = 1;
    public float range = 10;
    public LayerMask target;
    public LayerMask ignore;

    public float radius = 0;

    public bool guided = false;
    public Vector3 targetPosition;
    public bool nuke = false;

    public GameObject explosion;
    private float life;
    private bool destroyedCalled = false;

    private void Start()
    {
        ResetBullet();
    }

    private void Update()
    {
        life -= Time.deltaTime;
        this.transform.forward = rb.velocity.normalized;
        if (guided)
        {
            Vector3 v = (targetPosition - this.transform.position) * Time.deltaTime;
            v.y *= 4;
            rb.velocity += v;
        }
        if (life <= 0)
        {
            DestroyBullet();
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        GameObject go = other.collider.attachedRigidbody ? other.collider.attachedRigidbody.gameObject : other.gameObject;
        if (!ignore.Contains(other.gameObject.layer) /*&& this.tag != go.tag */&& target.Contains(go.layer))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (radius == 0)
            {
                if (damageable != null)
                {
                    damageable.Damage(damage);
                    Debug.Log("toto");
                }
            }
            else
            {
                Collider[] hits = Physics.OverlapSphere(this.transform.position, radius, target);
                HashSet<GameObject> hitted = new HashSet<GameObject>();
                foreach (Collider hit in hits)
                {
                    GameObject go2 = hit.attachedRigidbody ? hit.attachedRigidbody.gameObject : hit.gameObject;
                    {
                        if (!hitted.Contains(go2))
                        {
                            hitted.Add(go2);
                            IDamageable damageable2 = go2.GetComponent<IDamageable>();
                            if (damageable2 != null)
                            {
                                damageable2.Damage(damage);
                            }
                        }
                    }
                }
            }
            DestroyBullet();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
        if (!ignore.Contains(other.gameObject.layer) /*&& this.tag != go.tag */&& target.Contains(go.layer))
        {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (radius == 0)
            {
                if (damageable != null)
                {
                    damageable.Damage(damage);
                }
            }
            else
            {
                Collider[] hits = Physics.OverlapSphere(this.transform.position, radius, target);
                HashSet<GameObject> hitted = new HashSet<GameObject>();
                foreach (Collider hit in hits)
                {
                    GameObject go2 = hit.attachedRigidbody ? hit.attachedRigidbody.gameObject : hit.gameObject;
                    {
                        if (!hitted.Contains(go2))
                        {
                            hitted.Add(go2);
                            IDamageable damageable2 = go2.GetComponent<IDamageable>();
                            if (damageable2 != null)
                            {
                                damageable2.Damage(damage);
                            }
                        }
                    }
                }
            }
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        if(!destroyedCalled)
        {
            if (nuke)
            {

                EffectManager.main.Nuke(this.transform.position);
            }
            else
            {
                EffectManager.main.Hit(this.transform.position);
            }

            StartCoroutine(DoDie());
        }
        destroyedCalled = true;
    }

    IEnumerator DoDie()
    {
        if(explosion != null)
        {
            GameObject e = Instantiate(explosion);
            e.transform.position = transform.position;
            e.transform.parent = null;
            e.SetActive(true);
            e.GetComponent<Explosion>().Init();
        }

        rb.isKinematic = true;
        this.enabled = false;
        if(mesh)mesh.SetActive(false);
       
        float t = 1;
        float startWidth = trail ? trail.startWidth : 0;
        float endWidth = trail ? trail.endWidth : 0;
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        
        while (t >= 0 && trail != null)
        {
            t -= Time.deltaTime * 1;
            trail.startWidth = startWidth * t;
            trail.endWidth = endWidth * t;
            trail.time = (5 * t);
            yield return wait;
        }

        if(pooled)
            gameObject.SetActive(false);
        else
            Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        if (radius > 0)
        {
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
    }

    public void ResetBullet()
    {
        life = range;
        rb.isKinematic = false;
        enabled = true;
        if (mesh != null)
            mesh.SetActive(true);
        trail.Clear();
        trail.startWidth = 0.1f;
        trail.time = 1;
        destroyedCalled = false;
    }
}
