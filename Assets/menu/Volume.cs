using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class Volume : MonoBehaviour
{
    public AudioMixer mixer;

    public void SetVolume (float value)
    {
        mixer.SetFloat("BGM Volume", Mathf.Log10(value) * 20);
    }
}
