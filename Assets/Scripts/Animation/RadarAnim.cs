using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarAnim : MonoBehaviour
{
    [SerializeField] public Transform pivot1;
    [SerializeField] public Transform pivot2;
    [SerializeField] [Range(3, 20)] public float idleTime;
    [SerializeField] [Range(1, 100)] public float speed;
    private AudioSource audioSource;
    public AudioClip clipAudio;

    public float time, nextTime;
    private float u, v;
    void Start()
    {
        time = 0;
        nextTime = 1;
        u = 0; v = 0;
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > nextTime)
        {
            time = 0;
            nextTime = Random.Range(0, idleTime) + 3;
            u = Random.Range(0,360);
            v = Random.Range(0, 40);
            if (Random.Range(0, 1.0f) < 0.3f)
                audioSource.PlayOneShot(clipAudio);
        }

        pivot1.localEulerAngles = Vector3.MoveTowards(pivot1.localEulerAngles, new Vector3(0, u, 0), speed * Time.deltaTime);
        pivot2.localEulerAngles = Vector3.MoveTowards(pivot2.localEulerAngles, new Vector3(v, 0, 0), speed * Time.deltaTime);
    }
}
