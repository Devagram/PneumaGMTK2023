using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationController : MonoBehaviour
{
    [SerializeField]
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(name + "");
    }

    public void PlayIdleAnimation()
    {
        Debug.Log(name + "IdleTrigger");
        animator.SetTrigger("IdleTrigger");
    }

    public void PlayRunAnimation()
    {
        Debug.Log(name + "RunTrigger");
        animator.SetTrigger("RunTrigger");
    }

    public void PlayAttackAnimation()
    {
        Debug.Log(name + "AttackTrigger");
        animator.SetTrigger("AttackTrigger");
    }

    public void PlayHurtAnimation()
    {
        Debug.Log(name + "HurtTrigger");
        animator.SetTrigger("HurtTrigger");
    }

    public void PlayDeathAnimation()
    {
        Debug.Log(name + "DeathTrigger");
        animator.SetTrigger("DeathTrigger");
    }
}
