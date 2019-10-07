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
    public GameObject particlePrefab;
    [Range(0, 100)] public float ejectSpeed;
    public BoltAnim[] boltToInit;

    public void Init()
    {
        life = lifeTime;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(clipExplosion);
        for(int i=0; i<particleCount; i++)
        {
            GameObject b2 = Instantiate(particlePrefab);
            b2.GetComponent<BoltAnim>().lifeTime = lifeTime;
            b2.GetComponent<BoltAnim>().Init();
            b2.SetActive(true);
            b2.transform.position = transform.position;
            b2.transform.localEulerAngles = new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10));
            b2.transform.localScale = 0.1f * Random.Range(1.0f, 2) * Vector3.one;
            b2.GetComponent<Rigidbody>().velocity = ejectSpeed * Random.Range(0, 0.1f) * Vector3.up;
        }
        foreach (BoltAnim b in boltToInit)
            b.Init();
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
