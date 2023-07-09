using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayClick : MonoBehaviour
{
    [SerializeField]
    public AudioSource audioSource;
    public AudioClip mouseClick;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("click"); 
            audioSource.PlayOneShot(mouseClick);
        }
    }

}
