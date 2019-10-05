using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum Type
    {
        Gun,
        Minigun,
        Laser,
        Plasma
    };

    // Attributes
    [Header("Weapon")]
    [SerializeField] public int capacity;
    [SerializeField] public int load;
    [SerializeField] public float loadTime;
    [SerializeField] public bool reloadInterruptSupported = false;
    [SerializeField] public Type type;

    [Header("Shooting")]
    [SerializeField] public int bulletsPerShot = 1;
    [SerializeField] public float spread = 0;
    [SerializeField] public float cooldown = 0;

    [Header("Links")]
    [SerializeField] Bullet bulletPefab;
    [SerializeField] Transform nossle;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time-lastFire > 2)
        {
            if(load < capacity)
            {
                Reload();
            }
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
    bool failShot = false;
    float lastFire = 0;
    public void Fire()
    {
        if (load > 0)
        {
            StartCoroutine(DoFire());
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
    public bool firing = false;
    IEnumerator DoFire()
    {
        if (!firing)
        {

            firing = true;
            if (reloading && reloadInterruptSupported)
            {
                CancelReload();
            }
            if (!reloading)
            {
                for (int c = 0; c < bulletsPerShot; ++c)
                {
                   
                    Bullet b = GameObject.Instantiate<Bullet>(bulletPefab);
                    b.tag = owner.tag;
                    b.gameObject.SetActive(true);
                    yield return new WaitForEndOfFrame();
                    b.transform.position = nossle.position;
                    b.transform.rotation = nossle.rotation;
                    b.transform.RotateAround(nossle.position, Vector3.up, Random.Range(-spread, spread));
                   
                    b.rb.velocity = b.transform.forward * b.velocity /** Random.Range(0.8f, 1.2f)*/;
                    Debug.DrawRay(b.transform.position, b.rb.velocity);
                }
                --load;
                PlaySound(clipFire);
                
                float t = 0;
                float r = 0;
                /*while (t < cooldown / 2)
                {
                    r = Mathf.MoveTowards(r, 30, 30 * cooldown / 2);
                    this.transform.parent.localEulerAngles = new Vector3(0, 0, r);
                    yield return new WaitForEndOfFrame();
                    t += Time.deltaTime;

                } while (t < cooldown)
                {
                    r = Mathf.MoveTowards(r, 0, 30 * cooldown / 2);
                    this.transform.parent.localEulerAngles = new Vector3(0, 0, r);
                    yield return new WaitForEndOfFrame();
                    t += Time.deltaTime;

                }
                this.transform.parent.localEulerAngles = new Vector3(0, 0, 0);
                */
                yield return new WaitForSeconds(cooldown);
                if (load == 0)
                {
                    Reload();
                }
            }
            firing = false;
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
        if(audioSource)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
