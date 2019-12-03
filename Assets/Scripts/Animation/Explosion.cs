using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float lifeTime;
    public float life;
    private AudioSource audioSource;
    public AudioClip clipExplosion;
    public int particleCount;
    public string particlePoolName;
    private ParticlePool particlePool;
    [Range(0, 100)] public float ejectSpeed;
    [Range(0, 0.5f)] public float dispertion;
   // public BoltAnim[] boltToInit;

    void Start()
    {
        
    }
    public void Init()
    {
        GameObject bp = GameObject.Find(particlePoolName);
        if (bp != null)
        {
            particlePool = bp.GetComponent<ParticlePool>();
            if (particlePool == null) Debug.LogError("Object " + particlePoolName + " has no component BulletPool");
        }
        else Debug.LogError("ParticlePool of name : " + particlePoolName + ", not found");

        life = lifeTime;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipExplosion);

        for(int i=0; i<particleCount; i++)
        {
            GameObject b2 = particlePool.Take();
            b2.GetComponent<BoltAnim>().lifeTime = lifeTime;
            b2.GetComponent<BoltAnim>().ResetParticle();
            b2.transform.position = transform.position + 0.7f * Vector3.up + new Vector3(Random.Range(-dispertion, dispertion), Random.Range(-dispertion, dispertion), Random.Range(-dispertion, dispertion));
            b2.transform.localEulerAngles = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            b2.transform.localScale = 0.1f * Random.Range(1.0f, 2) * Vector3.one;
            b2.GetComponent<Rigidbody>().velocity = ejectSpeed * Random.Range(1.0f, 1.1f) * Vector3.up;
            b2.SetActive(true);
        }
        //foreach (BoltAnim b in boltToInit)
        //    b.Init();
    }
    void Update()
    {
        life -= Time.deltaTime;
        if(life < 0)
        {
            Destroy(gameObject);
        }
    }
}
