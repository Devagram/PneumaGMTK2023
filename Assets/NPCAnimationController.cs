using System.Collections;
using System.Collections.Generic;
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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(name + "");
    }

    public void PlayIdleAnimation()
    {
        audioSource.PlayOneShot(idleClip);
        Debug.Log(name + "IdleTrigger");
        animator.SetTrigger("IdleTrigger");
    }

    public void PlayRunAnimation()
    {
        audioSource.clip = runClip;
        audioSource.loop = true;
        audioSource.Play();
        Debug.Log(name + "RunTrigger");
        animator.SetTrigger("RunTrigger");
    }

    public void PlayAttackAnimation()
    {
        audioSource.PlayOneShot(attackClip);
        Debug.Log(name + "AttackTrigger");
        animator.SetTrigger("AttackTrigger");
    }

    public void PlayHurtAnimation()
    {
        audioSource.PlayOneShot(hurtClip);
        Debug.Log(name + "HurtTrigger");
        animator.SetTrigger("HurtTrigger");
    }

    public void PlayDeathAnimation()
    {
        audioSource.PlayOneShot(dieClip);
        Debug.Log(name + "DeathTrigger");
        animator.SetTrigger("DeathTrigger");
    }
}
