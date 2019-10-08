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

    [Header("debug")]
    public Transform target;
    public float offset;
    private Vector3 direction;
    private Vector3 position;
    public float time, nextTime;
    public float lasertime, lasernextTime;
    private float u, v, w;

    private AudioSource audioSource;
    public AudioClip clipAudio;

    void Start()
    {
        time = 0; lasertime = 0;
        nextTime = 1; lasernextTime = 1;
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
        laser.enabled = true;
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
            lasertime += Time.deltaTime;
            if (time > nextTime)
            {
                time = 0;
                nextTime = Random.Range(0, idleTime) + 3;
                u = Random.Range(0, 360);
                v = Random.Range(0, 30);
                if (Random.Range(0, 1.0f) < 0.3f)
                    audioSource.PlayOneShot(clipAudio);
            }
            if(lasertime > lasernextTime)
            {
                lasertime = 0;
                lasernextTime = Random.Range(0, idleTime/4) + 0.3f;
                w = Random.Range(1.2f, 1.8f);
            }
            pivot.localRotation = Quaternion.RotateTowards(pivot.localRotation, Quaternion.Euler(new Vector3(-v, u, 0)), speed * Time.deltaTime);
            laser.SetPosition(1, Vector3.MoveTowards(laser.GetPosition(1), new Vector3(0, 0, w), Time.deltaTime));
        }
    }
}
