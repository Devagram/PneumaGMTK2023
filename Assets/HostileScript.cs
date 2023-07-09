using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileScript : MonoBehaviour
{
    public string heroTag = "Hero";
    public float moveSpeed = 3f;
    public int attackDamage = 10;
    public HealthScript hp;
    //public NPCAnimationController animContrl;
    public float attackInterval = 2f;

    private GameObject hero;
    private HealthScript heroHP;
    private bool isAttacking = false;
    private int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking)
        {
            //animContrl.PlayIdleAnimation();
            FindHero();
            if (hero != null)
            {
                MoveToHero();
            }
        }
    }

    private void FindHero()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag(heroTag);
        if (heroes.Length > 0)
        {
            
            hero = heroes[0];
            //Debug.Log(name + "HERO FOUND" + hero.name);
            heroHP = hero.GetComponent<HealthScript>();
        }
        else
        {
            hero = null;
        }
    }

    private void MoveToHero()
    {
        transform.position = Vector2.MoveTowards(transform.position, hero.transform.position, moveSpeed * Time.deltaTime);
        //animContrl.PlayRunAnimation();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(heroTag))
        {
            isAttacking = true;
            //animContrl.PlayAttackAnimation();
            StartCoroutine(AttackRoutine());
        }
    }

    public void AttackHero()
    {
        if (hero != null)
        {
            //HealthScript heroHealth = hero.GetComponent<HealthScript>();
            if (heroHP != null)
            {
                heroHP.Damage(attackDamage);
            }
        }
    }

    private System.Collections.IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackInterval);

            if (isAttacking && hero != null)
            {
                AttackHero();
            }
        }
    }
}
