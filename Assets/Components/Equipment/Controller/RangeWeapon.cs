using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Equipment
{
    public class RangeWeapon : InventoryElementController<RangeWeaponData>
    {
        #region Links
        [Header("Links")]
        [SerializeField] GameObject owner;
        [SerializeField] Transform[] nozzles;
        [SerializeField] string bulletPoolName = "unknown";
        private ParticlePool bulletPool; 
        #endregion

        #region Controller
        [Header("Controller parameters")]
        [SerializeField] float timeUntilAutoReload = 2;
        #endregion

        #region Events
        public delegate void WeaponEvent();
        public WeaponEvent OnShotTick;
        public WeaponEvent OnEmpty;
        public WeaponEvent OnReloadStart;
        public WeaponEvent OnReloadTick;
        public WeaponEvent OnReloadEnd;
        public WeaponEvent OnReloadCancel;
        #endregion
        
        #region State
        [Header("Status")]
        [SerializeField] public int load;
        [SerializeField] public bool overheat;
        [SerializeField] public bool firing;
        [SerializeField] public bool reloading = false;

        private float lastFire = 0;
        private float currentCooldown = 0;
        private int currentNossle = 0;
        #endregion

        // Use this for initialization
        void Start()
        {
            load = data.capacity;
            firing = false;
            overheat = false;

            bulletPool = ParticlePool.pools[bulletPoolName];
            if (bulletPool == null)
            {
                Debug.LogError("ParticlePool of name : " + bulletPoolName + ", not found");
            }
        }

        void Update()
        {
            if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
            if (currentCooldown < 0) currentCooldown = 0;

            if (Time.time - lastFire > timeUntilAutoReload)
            {
                if (load < data.capacity)
                {
                    Reload();
                }
            }
        }


        public void Fire()
        {
            if (load > 0)
            {
                DoFire();
                lastFire = Time.time;
            }
            else
            {
                Reload();
            }
        }

        public void Reload()
        {
            StartCoroutine(DoReload());

        }
        void CancelReload()
        {
            OnReloadCancel?.Invoke();
            StopCoroutine(DoReload());
            reloading = false;

        }
        #region Coroutines
        void DoFire()
        {
            if (currentCooldown == 0)
            {
                firing = true;

                if (reloading && data.reloadInterruptSupported && !overheat)
                {
                    CancelReload();
                }
                else if (!reloading)
                {
                    if (bulletPool != null)
                        for (int c = 0; c < data.bulletsPerShot; ++c)
                        {
                            Bullet b = bulletPool.Take().GetComponent<Bullet>();
                            if (b)
                            {
                                b.tag = owner.tag;

                                Transform nossle = nozzles[currentNossle];
                                b.transform.position = nossle.position;
                                b.transform.rotation = nossle.rotation;
                                b.transform.RotateAround(nossle.position, Vector3.up, Random.Range(-data.spread / 2, data.spread / 2));
                                b.rb.velocity = b.transform.forward * b.data.velocity;

                                b.ResetParticle();
                                b.gameObject.SetActive(true);

                                currentNossle = (currentNossle + 1) % nozzles.Length;


                                OnShotTick?.Invoke();
                            }
                            else
                            {
                                Debug.LogWarning("no bullet");
                            }
                        }

                    load -= data.cost;
                    currentCooldown = data.cooldown;
                    if (load < data.cost)
                    {
                        OnEmpty?.Invoke();
                        overheat = true;
                        Reload();
                    }
                }
            }
        }
        IEnumerator DoReload()
        {
            if (!reloading)
            {
                OnReloadStart?.Invoke();


                OnEmpty?.Invoke();

                reloading = true;
                while (load < data.capacity && reloading)
                {
                    OnReloadTick?.Invoke();
                    if (reloading)
                    {
                        load++;
                        yield return new WaitForSeconds(data.loadTime);
                    }
                }
                reloading = false;
                overheat = false;

                OnReloadEnd?.Invoke();
            }
        }
        #endregion

        public string GetLiteralType()
        {
            switch (data.weaponType)
            {
                case RangeWeaponData.Type.Minigun: return "minigun";
                default: return "plasma";
            }
        }
        public void SetActive(bool active)
        {
            if (active == false)
            {
                reloading = false;
                this.gameObject.SetActive(false);
            }
            else
            {
                load = data.capacity;
                this.gameObject.SetActive(true);
            }
        }
        public bool IsActive()
        {
            return gameObject.activeSelf;
        }
    }
}