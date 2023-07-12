using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    [SerializeField]
    public AudioSource AudioSource;
    public void PlayAudio()
    {
        AudioSource.Play();
    }
}
