using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{
    [SerializeField]
    public AudioMixer mixer;
    [SerializeField]
    public String mixerExposedVal;
    [SerializeField]
    public float defaultVal;

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat(mixerExposedVal, Mathf.Log10(sliderValue)*20);
    }
}
