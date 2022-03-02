using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReact : MonoBehaviour
{

    AudioSource audioSource;

    public static float[] spectrumData = new float[512];

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.Hanning);
    }
}
