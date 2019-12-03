using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : Particle
    {
        [SerializeField] public BulletData data;
        #region Links
        [Header("Links")]
        public bool pooled = true;
        public TrailRenderer trail;
        public GameObject mesh;
        public Rigidbody rb;
        public GameObject explosion;
        #endregion

        #region State
        [Header("State")]
        public bool guided = false;
        public Vector3 targetPosition;
        private float life;
        private bool destroyedCalled = false;
        #endregion

        protected override void DoCreate()
        {
            lifeTime = data.range;
            rb.isKinematic = false;
            enabled = true;
            if (mesh != null)
                mesh.SetActive(true);
            trail.Clear();
            trail.startWidth = 0.1f;
            trail.time = 1;
            destroyedCalled = false;
            OnCreate?.Invoke();
        }

        private void OnCollisionEnter(Collision other)
        {
            GameObject go = other.collider.attachedRigidbody ? other.collider.attachedRigidbody.gameObject : other.gameObject;
            if (!data.ignore.Contains(other.gameObject.layer) /*&& this.tag != go.tag */&& data.target.Contains(go.layer))
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (data.radius == 0)
                {
                    if (damageable != null)
                    {
                        damageable.Damage(data.damage);
                    }
                }
                else
                {
                    Collider[] hits = Physics.OverlapSphere(this.transform.position, data.radius, data.target);
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
                                    damageable2.Damage(data.damage);
                                }
                            }
                        }
                    }
                }
                DoDestroy();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            GameObject go = other.attachedRigidbody ? other.attachedRigidbody.gameObject : other.gameObject;
            if (!data.ignore.Contains(other.gameObject.layer) /*&& this.tag != go.tag */&& data.target.Contains(go.layer))
            {
                IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
                if (data.radius == 0)
                {
                    if (damageable != null)
                    {
                        damageable.Damage(data.damage);
                    }
                }
                else
                {
                    Collider[] hits = Physics.OverlapSphere(this.transform.position, data.radius, data.target);
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
                                    damageable2.Damage(data.damage);
                                }
                            }
                        }
                    }
                }
                DoDestroy();
            }
        }

        protected override void DoDestroy()
        {
            if (!destroyedCalled)
            {
                if (data.nuke)
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
            if (explosion != null)
            {
                GameObject e = Instantiate(explosion);
                e.transform.position = transform.position;
                e.transform.parent = null;
                e.SetActive(true);
                e.GetComponent<Explosion>().Init();
            }

            rb.isKinematic = true;
            this.enabled = false;
            if (mesh) mesh.SetActive(false);

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

            if (pooled)
                gameObject.SetActive(false);
            else
                Destroy(this.gameObject);
        }
        private void OnDrawGizmos()
        {
            if (data.radius > 0)
            {
                Gizmos.DrawWireSphere(this.transform.position, data.radius);
            }
        }


        protected override void DoTick()
        {
            this.transform.forward = rb.velocity.normalized;
            if (guided)
            {
                Vector3 v = (targetPosition - this.transform.position) * Time.deltaTime;
                v.y *= 4;
                rb.velocity += v;
            }
        }
    }

}