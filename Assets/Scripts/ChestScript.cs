
using UnityEngine;

public class ChestScript : MonoBehaviour
{
    LevelHandler lvlHandeler;
    [SerializeField]
    Animator animator;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip openClip;
    [SerializeField]
    AudioClip closeClip;

    [SerializeField]
    bool chestOpen = false;

    [SerializeField]
    Collider2D chestCollider;
    // Start is called before the first frame update
    void Start()
    {
        lvlHandeler = GameObject.Find("LevelManager").GetComponent<LevelHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chestOpen)
        {
            chestCollider.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //collidedWith = col;
        //Debug.Log("Trap collided with: " + col.name);
        //Non-possessable t raps don't affect the playable ghost
        NewHeroWanderScript wanderScript;
        if (!chestOpen && col.gameObject.tag == "Hero")
        {
            wanderScript = col.gameObject.GetComponent<NewHeroWanderScript>();
            OnTriggered();
            wanderScript.hasGoal = false;
        }
        
    }
    void OnTriggered()
    {
        float goldToRemove = 50f;
        gameObject.tag = "Done";
        Debug.Log("CHEST TRIGGERED");
        chestOpen = true;
        animator.SetTrigger("Opened");
        audioSource.PlayOneShot(openClip);
        lvlHandeler.RemoveGold(goldToRemove);
    }
}
