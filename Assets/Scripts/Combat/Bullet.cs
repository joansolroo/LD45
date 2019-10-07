using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    public TrailRenderer trail;
    public GameObject mesh;
    public Rigidbody rb;

    public int velocity = 1;
    public int damage = 1;
    public float ttl = 10;
    public LayerMask target;
    public LayerMask ignore;

    public float radius = 0;

    public bool guided = false;
    public Vector3 targetPosition;
    public bool nuke = false;
    public void Start()
    {
        //ttl *= Random.Range(0.95f, 1.05f);
    }
    private void Update()
    {
        ttl -= Time.deltaTime;
        this.transform.forward = rb.velocity.normalized;
        if (guided)
        {
            Vector3 v = (targetPosition - this.transform.position) * Time.deltaTime;
            v.y *= 4;
            rb.velocity += v;
        }
        if (ttl <= 0)
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

    IEnumerator DoDie()
    {
        rb.isKinematic = true;
        this.enabled = false;
        mesh.SetActive(false);
       
        float t = 1;
        float startWidth = trail.startWidth;
        float endWidth = trail.endWidth;
        WaitForEndOfFrame wait = new WaitForEndOfFrame();
        
        while (t >= 0 && trail != null)
        {
            t -= Time.deltaTime * 1;
            trail.startWidth = startWidth * t;
            trail.endWidth = endWidth * t;
            trail.time = (5 * t);
            yield return wait;

        }

        Destroy(this.gameObject);
    }
    private void OnDrawGizmos()
    {
        if (radius > 0)
        {
            Gizmos.DrawWireSphere(this.transform.position, radius);
        }
    }
}
