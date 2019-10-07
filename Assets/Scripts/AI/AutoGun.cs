using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : MonoBehaviour
{
    [Header("Attributes")]
    private Sense sight;
    public Transform pivot;
    public LineRenderer laser;
    private Weapon weapon;
    public float aimRange = 5f;
    public float shootRange = 4f;
    [Range(3, 20)] public float idleTime;
    [Range(1, 100)] public float speed;
    [Range(0, 1)] public float laserNoise;
    [Range(0, 1)] public float laserMin;

    [Header("debug")]
    public Transform target;
    public float offset;
    private Vector3 direction;
    private Vector3 position;
    public float time, nextTime;
    private float u, v, w;

    private AudioSource audioSource;
    public AudioClip clipAudio;

    void Start()
    {
        time = 0;
        nextTime = 1;
        weapon = GetComponent<Weapon>();
        sight = GetComponent<Sense>();
        audioSource = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        //  sens
        GameObject go = sight.GetClosest(transform.position);
        if (go) target = go.transform;
        else target = null;

        if (target)
        {
            Vector3 direction = target.position - this.transform.position;
            if (direction.magnitude < aimRange)
            {
                position = target.position + offset * Vector3.up;
            }
        }

        // act
        w = (1 - laserNoise) * w + laserNoise * Random.Range(laserMin, 1.0f);
        laser.enabled = w > 0.6f;
        if (target)
        {
            if (direction.magnitude < aimRange)
            {
                pivot.LookAt(position);
                if (direction.magnitude < shootRange)
                {
                    weapon.Fire();
                    laser.enabled = false;
                }
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time > nextTime)
            {
                time = 0;
                nextTime = Random.Range(0, idleTime) + 3;
                u = Random.Range(0, 360);
                v = Random.Range(0, 30);
                if (Random.Range(0, 1.0f) < 0.3f)
                    audioSource.PlayOneShot(clipAudio);
            }
            pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, Quaternion.Euler(new Vector3(-v, u, 0)), speed * Time.deltaTime);
        }
    }
}
