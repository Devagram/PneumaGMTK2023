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
        // Debug.Log(name + "");
    }

    public void PlayIdleAnimation()
    {
        animator.SetTrigger("IdleTrigger");
    }

    public void PlayRunAnimation()
    {
        animator.SetTrigger("RunTrigger");
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger("AttackTrigger");
    }

    public void PlayHurtAnimation()
    {
        animator.SetTrigger("HurtTrigger");
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger("DeathTrigger");
    }
}
