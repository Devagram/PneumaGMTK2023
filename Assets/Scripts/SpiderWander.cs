using System.Collections;
//using Unity.VisualScripting;
using UnityEngine;
//using static UnityEditor.VersionControl.Asset;

public class SpiderWander : MonoBehaviour
{
    public float moveForce = 5f;
    public float wallDetectionDistance = 1f;
    public float goalDetectionDistance = 5f;
    public float raycastCooldown = 2f;
    public GameObject goalObject;
    public float visionAngle = 90f;
    private Rigidbody2D rb;
    public bool hasGoal;
    //public LayerMask obstacleLayer;
    public LayerMask goalLayer;
    //private Collider2D aiCollider;
    private float timer = 0f;
   //private bool canRaycast = true;
    private Vector2 currentDirection;
    public float maxSpeed = 5f;

    private float waitCounter = 3f;
    private bool waiting = false;

    private Coroutine initialWaitCoroutine;
    private Coroutine waitCoroutine;

    [SerializeField]
    NPCAnimationController animController;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasGoal = false;
        //aiCollider = GetComponent<Collider2D>();
        currentDirection = Vector2.down;
    }

    private void Update()
    {
        /*if (hasGoal)
        {
            //MoveTowardsGoal();
        }
        else
        {*/
            DetectGoal();
        //}
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    private void MoveTowardsGoal()
    {
        Vector2 direction = (goalObject.transform.position - transform.position).normalized;
        rb.AddForce(direction * moveForce);
        //rb.velocity = direction * moveSpeed;
    }

    private void DetectGoal()
    {
        Collider2D[] goals = Physics2D.OverlapCircleAll(transform.position, goalDetectionDistance);
        //Debug.Log(goals[0]);
        foreach (Collider2D goal in goals)
        {
            Debug.Log("GOAL LOOP: " + goal.gameObject.name +" & tagged as " + goal.gameObject.tag);
            if (goal.gameObject.tag == "Goal")
            {
                //Debug.Log("GOAL LOOP: " + goal.gameObject.name);
                Vector2 directionToGoal = goal.transform.position - transform.position;
                Debug.DrawRay(transform.position, transform.forward * goalDetectionDistance, Color.red);
                //RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToGoal, goalDetectionDistance);
                //if (hit.collider == null)
                //{
                hasGoal = true;
                goalObject = goal.gameObject;
                break;
                //}
            }
        }
        //Debug.Log("HASGOAL + " + hasGoal);
        if (!hasGoal)
        {
            Wander();
        } else {
            MoveTowardsGoal();
        }
    }

    private void Wander()
    {
        //Debug.Log("CAN RAYCAST: " + canRaycast);
        //if (canRaycast)
        //{
        //Debug.Log("Wandering");
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, wallDetectionDistance);
        //Debug.DrawRay(transform.position, Vector2.right * wallDetectionDistance, Color.red);

        /*if (hit.collider != null)
        {
            Debug.Log("Hit! + " + hit.collider.gameObject);
            float randomTurnAngle = Random.Range(270f, 90f);
            transform.Rotate(0f, 0f, randomTurnAngle);
            timer = 0f;
            canRaycast = false;
        }
        else
        {
            rb.velocity = transform.right * moveSpeed;
        }*/
        /*}
        else
        {*/
        //Debug.Log("Timer: " + timer);
        if(!waiting) {
            timer += Time.deltaTime;
            if (timer >= raycastCooldown)
            {
                timer = 0f;
                //canRaycast = true;
            }
            rb.AddForce(currentDirection * moveForce);

            //rb.AddForce(currentDirection * moveForce, ForceMode2D.Force);
            //rb.velocity = currentDirection * moveSpeed;
            if (animController.animationState != "running") {
                animController.PlayRunAnimation();
            }
        } else
        {

            if (animController.animationState != "idleing")
            {
                waitCoroutine = StartCoroutine(InitialWait());
                animController.PlayIdleAnimation();
                waitCoroutine = StartCoroutine(Wait());
            }
        }
        //}
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision enter event
        // Access collision information and perform actions
        //Debug.Log("Hit! + " + collision.gameObject);
        float randomTurnAngle = Random.Range(90f, 35f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, randomTurnAngle);
        currentDirection  = rotation * currentDirection;
        timer = 0f;
        waitCounter--;
        //canRaycast = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Handle collision enter event
        // Access collision information and perform actions
        //Debug.Log("Hit! + " + collision.gameObject);
        float randomTurnAngle = Random.Range(270f, 90f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, randomTurnAngle);
        currentDirection = rotation * currentDirection;
        timer = 0f;
        //canRaycast = false;
    }

    /* void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == goalObject)
        {
            goalObject.SetActive(false);
            hasGoal = false;
        }
    }*/

    private IEnumerator InitialWait()
    {
        float timer = 5f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //timerText.text = Mathf.RoundToInt(timer).ToString();

            yield return null;
        }
        waiting = true;
    }

    public IEnumerator Wait()
    {
        Debug.Log("Wait Initiated");
        waiting = true;
        float timer = 10f;
        while (timer > 0)
        {
            timer -= Time.deltaTime;
            //timerText.text = Mathf.RoundToInt(timer).ToString();

            yield return null;
        }
        waiting = false;
    }
}