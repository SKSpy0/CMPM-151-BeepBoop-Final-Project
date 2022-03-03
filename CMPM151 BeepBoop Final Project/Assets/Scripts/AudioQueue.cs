using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioQueue : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] private float delay;
    [SerializeField] private AudioClip[] clips;

    float waitfor = 0f;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSource.isPlaying)
        {
            int i = Random.Range(0,clips.Length);
            //Debug.Log(i);
            nextclip(clips[i]);
        }
        waitfor-=Time.deltaTime;
    }

    void nextclip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip,1f);
        waitfor = clip.length;
    }
}
