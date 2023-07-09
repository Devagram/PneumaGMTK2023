using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDefendScript : MonoBehaviour
{


    private GameObject mob;
    private HealthScript mobHP;
    private bool isAttacking = false;
    public float attackInterval = 2f;
    public int attackDamage = 10;
    public NPCAnimationController animContrl;
    public string hostileTag;

    // Update is called once per frame
    void Start()
    {
        if (!isAttacking)
        {
            animContrl.PlayIdleAnimation();
        }
    }

    private System.Collections.IEnumerator DefendRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            if (isAttacking && mob != null)
            {
                AttackMob();
            }
        }
    }

    public void AttackMob()
    {
        if (mob != null)
        {
            //HealthScript heroHealth = hero.GetComponent<HealthScript>();
            if (mob != null)
            {
                mobHP.Damage(attackDamage);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag(hostileTag))
        {
            //Debug.Log("Bump found with tag: " + collision.gameObject.tag);
            isAttacking = true;
            mob = collision.gameObject;
            mobHP = collision.gameObject.GetComponent<HealthScript>();  
            animContrl.PlayAttackAnimation();
            StartCoroutine(DefendRoutine());
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(hostileTag))
        {
            isAttacking = false;
            animContrl.PlayRunAnimation();
        }
    }
}
