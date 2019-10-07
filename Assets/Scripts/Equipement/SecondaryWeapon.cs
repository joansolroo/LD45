using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    public enum Type
    {
        Rockets,
        OrbitalLaser
    };

    // Attributes
    [Header("SecondaryWeapon")]
    [SerializeField] public Type type;
    [SerializeField] public int alignment;
    [SerializeField] public GameObject iconPrefab;
    
    [Header("Weapon")]
    [SerializeField] public int capacity;
    [SerializeField] public int load;
    [SerializeField] public float loadTime;
    [SerializeField] public bool reloadInterruptSupported = false;
    [SerializeField] GuidingSystem guiding;

    [Header("Shooting")]
    [SerializeField] public float cooldown = 0;
    [SerializeField] public int cost = 1;
    [SerializeField] public bool overheat;
    [SerializeField] public bool firing;
    private int lastFireFrame;

    [Header("Links")]
    [SerializeField] Bullet bulletPefab;
    [SerializeField] Transform[] nossles;
    [SerializeField] GameObject owner;

    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clipFire;

    [SerializeField] int hand;


    void Start()
    {
        firing = false;
        overheat = false;
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
        if (currentCooldown <= 0 && lastFireFrame != Time.frameCount)
        {
            firing = false;
        }
    }

    public string GetLiteralType()
    {
        switch (type)
        {
            case Type.Rockets: return "rockets";
            default: return "orbital laser";
        }
    }
    public void SetActive(bool active)
    {
        if (active == false)
        {
            firing = false;
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

    float lastFire = 0;
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

    float currentCooldown = 0;
    void DoFire()
    {
        if (currentCooldown == 0)
        {
            firing = true;
            lastFireFrame = Time.frameCount;

            if (reloading && reloadInterruptSupported)
            {
                CancelReload();
            }
            if (!reloading)
            {
                audioSource.PlayOneShot(clipFire);

                Bullet b = GameObject.Instantiate<Bullet>(bulletPefab);
                b.tag = owner.tag;
                b.gameObject.SetActive(true);
                if (guiding)
                {
                    guiding.targetPos = Cursor.main.targetPosition;
                    guiding.Guide(b);
                }
                Transform nossle = nossles[Random.Range(0, nossles.Length)];
                b.transform.position = nossle.position;
                b.transform.rotation = nossle.rotation;
                b.rb.velocity = b.transform.forward * b.velocity;
                Debug.DrawRay(b.transform.position, b.rb.velocity);
                
                load -= cost;
                if (load < cost)
                {
                    overheat = true;
                }

                PlaySound(clipFire);
                currentCooldown = cooldown;

                if (load == 0)
                {
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
            reloading = true;
            while (load < capacity && reloading)
            {
                if (reloading)
                {
                    load++;
                    yield return new WaitForSeconds(loadTime);
                }
            }
            reloading = false;
            overheat = false;
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
