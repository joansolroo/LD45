using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        Minigun,
        Plasma,
        Autogun
    };

    // Attributes
    [Header("Weapon")]
    [SerializeField] public int capacity;
    [SerializeField] public int load;
    [SerializeField] public float loadTime;
    [SerializeField] public bool reloadInterruptSupported = false;
    [SerializeField] public Type type;
    [SerializeField] public int alignment;
    [SerializeField] public GameObject iconPrefab;

    [Header("Shooting")]
    [SerializeField] public int bulletsPerShot = 1;
    [SerializeField] public float spread = 0;
    [SerializeField] public float cooldown = 0;
    [SerializeField] public int cost = 1;
    [SerializeField] public bool overheat;
    [SerializeField] public bool firing;
    [SerializeField] public bool flame;
    private int lastFireFrame;

    [Header("Links")]
    [SerializeField] Bullet bulletPefab;
    [SerializeField] Transform[] nossles;
    [SerializeField] GameObject owner;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clipFire;
    [SerializeField] AudioClip clipEmpty;
    [SerializeField] AudioClip clipReload;

    [SerializeField] int hand;

    public delegate void WeaponEvent();
    public WeaponEvent OnBulletShot;
    // Use this for initialization
    void Start()
    {
        flame = false;
        firing = false;
        overheat = false;
        lastFireFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCooldown > 0) currentCooldown -= Time.deltaTime;
        if (currentCooldown < 0) currentCooldown = 0;

        if (Time.time - lastFire > 2)
        {
            if (load < capacity)
            {
                Reload();
            }
        }
    }
    private void LateUpdate()
    {
        flame = false;
        if (currentCooldown <= 0 && lastFireFrame != Time.frameCount)
        {
            flame = lastFireFrame != Time.frameCount;
            firing = false;
        }
    }

    public string GetLiteralType()
    {
        switch(type)
        {
            case Type.Minigun: return "minigun";
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
            load = capacity;
            this.gameObject.SetActive(true);
        }
    }
    public bool IsActive()
    {
        return gameObject.activeSelf;
    }

    private float lastFire = 0;
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
    int currentNossle = 0;
    private float currentCooldown = 0;
    void DoFire()
    {
        if (currentCooldown == 0)
        {
            firing = true;
            lastFireFrame = Time.frameCount;

            

            if (reloading && reloadInterruptSupported && !overheat)
            {
                CancelReload();
            }
            else if (!reloading)
            {
                audioSource.loop = false;
                audioSource.volume = 1;
                audioSource.PlayOneShot(clipFire);

                for (int c = 0; c < bulletsPerShot; ++c)
                {

                    Bullet b = GameObject.Instantiate<Bullet>(bulletPefab);
                    b.tag = owner.tag;
                    b.gameObject.SetActive(true);
                    Transform nossle = nossles[currentNossle];
                    b.transform.position = nossle.position;
                    b.transform.rotation = nossle.rotation;
                    b.transform.RotateAround(nossle.position, Vector3.up, Random.Range(-spread, spread));

                    b.rb.velocity = b.transform.forward * b.velocity;
                    Debug.DrawRay(b.transform.position, b.rb.velocity);
                    currentNossle = (currentNossle + 1) % nossles.Length;

                    if (OnBulletShot != null)
                        OnBulletShot();
                }

                load -= cost;                
                currentCooldown = cooldown;
                if (load < cost)
                {
                    overheat = true;
                    Reload();
                }
            }
        }
    }


    public void Reload()
    {
        StartCoroutine(DoReload());
        
    }
    void CancelReload()
    {
        StopCoroutine(DoReload());
        reloading = false;
        
    }
    public bool reloading = false;

    IEnumerator DoReload()
    {
        if (!reloading)
        {
            if (!overheat)
            {
                audioSource.clip = clipReload;
            }
            else
            {
                audioSource.clip = clipEmpty;
            }
            audioSource.loop = true;
            audioSource.Play();

            reloading = true;
            while (load < capacity && reloading)
            {
                audioSource.volume = Mathf.Min(5*(1.0f - (float)load / capacity), 1.0f);
                if (reloading)
                {
                    load++;
                    yield return new WaitForSeconds(loadTime);
                }
            }
            reloading = false;
            overheat = false;
            audioSource.loop = false;
            audioSource.Stop();
            audioSource.volume = 1;
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
