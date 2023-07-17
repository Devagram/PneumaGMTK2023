using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    [SerializeField]
    public AudioSource audioSource;
    [SerializeField]
    public AudioClip idleClip;
    [SerializeField]
    public AudioClip runClip;
    [SerializeField]
    public AudioClip attackClip;
    [SerializeField]
    public AudioClip hurtClip;
    [SerializeField]
    public AudioClip dieClip;
    [SerializeField]
    public AudioClip spawnClip;

    public string animationState;
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(name + "");
        //audioSource.loop = false;
        //audioSource.clip = spawnClip;
        audioSource.PlayOneShot(spawnClip);
    }

    public void PlayIdleAnimation()
    {
        animationState = "idleing";
        animator.SetTrigger("IdleTrigger");
        audioSource.Stop();
    }

    public void PlayRunAnimation()
    {
        animationState = "running";
        animator.SetTrigger("RunTrigger");
        audioSource.loop = true;
        audioSource.clip = runClip;
        audioSource.Play();
    }

    public void PlayAttackAnimation()
    {
        animationState = "attacking";
        animator.SetTrigger("AttackTrigger");
    }

    public void PlayHurtAnimation()
    {
        animationState = "hurting";
        animator.SetTrigger("HurtTrigger");
    }

    public void PlayDeathAnimation()
    {
        animationState = "dieing";
        animator.SetTrigger("DeathTrigger");
    }
}
