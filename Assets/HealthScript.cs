using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public NPCAnimationController animContrl;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            //todo ad ienumerator that delays death and plays animation
            //animContrl.PlayDeathAnimation();
            Destroy(gameObject);
        }
    }

    public void SetHealth(int newHealth)
    {
        currentHealth = newHealth;
    }

    public void Damage(int damageAmt)
    {
        //Debug.Log(name + "DMGED" + damageAmt +"HP"+ currentHealth);
        currentHealth = currentHealth - damageAmt;
    }
}