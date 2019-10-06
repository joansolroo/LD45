using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        None,
        Gun,
        Minigun,
        Laser,
        Plasma,
        Granade
    };

    // Attributes
    [Header("Weapon")]
    [SerializeField] public int capacity;
    [SerializeField] public int load;
    [SerializeField] public float loadTime;
    [SerializeField] public bool reloadInterruptSupported = false;
    [SerializeField] public Type type;
    [SerializeField] public int alignment;

    [Header("Shooting")]
    [SerializeField] public int bulletsPerShot = 1;
    [SerializeField] public float spread = 0;
    [SerializeField] public float cooldown = 0;
    [SerializeField] public bool firing;
    public int lastFireFrame;

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
    // Use this for initialization
    void Start()
    {
        firing = false;
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
        if(currentCooldown<=0 && lastFireFrame != Time.frameCount)
        {
            firing = false;
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

    private bool failShot = false;
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
            {
                Reload();
                //   failShot = false;
            }
        }
    }

    private float currentCooldown = 0;
    void DoFire()
    {
        if (currentCooldown == 0)
        {
            firing = true;
            lastFireFrame = Time.frameCount;

            Debug.Log("do fire");
            if (reloading && reloadInterruptSupported)
            {
                CancelReload();
            }
            if (!reloading)
            {
                foreach (Transform nossle in nossles)
                {
                    for (int c = 0; c < bulletsPerShot; ++c)
                    {

                        Bullet b = GameObject.Instantiate<Bullet>(bulletPefab);
                        b.tag = owner.tag;
                        b.gameObject.SetActive(true);
                        //Transform nossle = nossles[Random.Range(0, nossles.Length)];
                        b.transform.position = nossle.position;
                        b.transform.rotation = nossle.rotation;
                        b.transform.RotateAround(nossle.position, Vector3.up, Random.Range(-spread, spread));

                        b.rb.velocity = b.transform.forward * b.velocity /** Random.Range(0.8f, 1.2f)*/;
                        Debug.DrawRay(b.transform.position, b.rb.velocity);
                    }
                }
                --load;
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
            //yield return new WaitForSeconds(loadTime/2);
            if (reloading)
            {
                PlaySound(clipReload);
                //sprite.color = new Color(1,0,0,0.5f);
                load++;
                yield return new WaitForSeconds(loadTime);
            }
        }
        reloading = false;
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
