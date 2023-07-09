using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolumes : MonoBehaviour
{
    [SerializeField]
    public AudioMixer mixer;
    [SerializeField]
    public String mixerExposedValMAST;
    [SerializeField]
    public String mixerExposedValUI;
    [SerializeField]
    public String mixerExposedValMUS;
    [SerializeField]
    public String mixerExposedValSFX;
    [SerializeField]
    public String mixerExposedValAMB;
    [SerializeField]
    public float defaultValMAST;
    [SerializeField]
    public float defaultValUI;
    [SerializeField]
    public float defaultValMUS;
    [SerializeField]
    public float defaultValSFX;
    [SerializeField]
    public float defaultValAMB;
    void Start()
    {
        mixer.SetFloat(mixerExposedValMAST, Mathf.Log10(defaultValMAST) * 20);
        mixer.SetFloat(mixerExposedValUI, Mathf.Log10(defaultValUI) * 20);
        mixer.SetFloat(mixerExposedValMUS, Mathf.Log10(defaultValMUS) * 20);
        mixer.SetFloat(mixerExposedValSFX, Mathf.Log10(defaultValSFX) * 20);
        mixer.SetFloat(mixerExposedValAMB, Mathf.Log10(defaultValAMB) * 20);
    }
}
