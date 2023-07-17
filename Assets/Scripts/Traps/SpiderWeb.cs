using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpiderWeb : MonoBehaviour
{
    private SpriteRenderer r;

    void Start()
    {
        r = GetComponent<SpriteRenderer>();
        r.flipX = Random.value >= 0.5f;
        r.flipY = Random.value >= 0.5f;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //if adventurer walks over it, apply debuff

        if (collision.tag == "Hero")
        {
            NewHeroWanderScript heroScript = collision.gameObject.GetComponentInChildren<NewHeroWanderScript>();
            heroScript.moveForce = heroScript.moveForce - .5f;
        }
    }
}
