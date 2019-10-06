using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] AudioSource[] audioSources;
    [SerializeField] public float pitch = 1;
    [SerializeField] [Range(0,1)] public float intensity = 1;
    [SerializeField] [Range(0, 1)] public float volume = 1;

    public float crowd = 0;
    [SerializeField] AudioSource crowdAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSources = new AudioSource[audioClips.Length];
        for(int a = 0; a < audioClips.Length;++a)
        {
            GameObject go = new GameObject();
            go.transform.parent = this.transform;
            audioSources[a] = go.AddComponent<AudioSource>();
            audioSources[a].clip = audioClips[a];
        }
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        while (true)
        {
            crowdAudioSource.volume = Mathf.MoveTowards(crowdAudioSource.volume, crowd, Time.deltaTime*2) ;
            if (audioSources[0].isPlaying)
            {
                audioSources[0].pitch = pitch;
                for (int a = 0; a < audioClips.Length; ++a)
                {
                    if (((float)a) / (audioClips.Length - 1) > intensity)
                    {
                        audioSources[a].volume = Mathf.MoveTowards(audioSources[a].volume, 0, Time.deltaTime * 5);
                        audioSources[a].Stop();
                    }
                }
                yield return null;
            }
            else
            {
                for (int a = 0; a < audioClips.Length; ++a)
                {
                    if (((float)a) / (audioClips.Length - 1) <= intensity)
                    {
                        audioSources[a].pitch = pitch;
                        audioSources[a].volume = volume;
                        audioSources[a].Play();
                    }
                }
            }
        }
    }
}
