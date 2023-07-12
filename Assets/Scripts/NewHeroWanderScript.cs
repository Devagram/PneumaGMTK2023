using UnityEngine;

public class NewHeroWanderScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float wallDetectionDistance = 1f;
    public float goalDetectionDistance = 5f;
    public float raycastCooldown = 2f;
    public GameObject goalObject;
    public float visionAngle = 90f;
    private Rigidbody2D rb;
    private bool hasGoal;
    //public LayerMask obstacleLayer;
    public LayerMask goalLayer;
    private Collider2D aiCollider;
    private float timer = 0f;
   //private bool canRaycast = true;
    private Vector2 currentDirection;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        hasGoal = false;
        aiCollider = GetComponent<Collider2D>();
        currentDirection = Vector2.right;
    }

    private void Update()
    {
        if (hasGoal)
        {
            MoveTowardsGoal();
        }
        else
        {
            DetectGoal();
        }
    }

    private void MoveTowardsGoal()
    {
        Vector2 direction = (goalObject.transform.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    private void DetectGoal()
    {
        //Collider2D[] goals = Physics2D.OverlapCircleAll(transform.position, goalDetectionDistance);
        //Debug.Log(goals[0]);
        /*foreach (Collider2D goal in goals)
        {
            Vector2 directionToGoal = goal.transform.position - transform.position;
            Debug.DrawRay(transform.position, transform.forward * goalDetectionDistance, Color.red);
            float angleToGoal = Vector2.Angle(transform.right, directionToGoal);

            if (angleToGoal <= visionAngle * 0.5f)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToGoal, goalDetectionDistance);
                if (hit.collider == null)
                {
                    hasGoal = true;
                    goalObject = goal.gameObject;
                    break;
                }
            }
        }*/
        //Debug.Log("HASGOAL + " + hasGoal);
        if (!hasGoal)
        {
            Wander();
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
            timer += Time.deltaTime;
            if (timer >= raycastCooldown)
            {
                timer = 0f;
                //canRaycast = true;
            }
            rb.velocity = currentDirection * moveSpeed;
        //}
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle collision enter event
        // Access collision information and perform actions
        Debug.Log("Hit! + " + collision.gameObject);
        float randomTurnAngle = Random.Range(90f, 35f);
        Quaternion rotation = Quaternion.Euler(0f, 0f, randomTurnAngle);
        currentDirection  = rotation * currentDirection;
        timer = 0f;
        //canRaycast = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        // Handle collision enter event
        // Access collision information and perform actions
        Debug.Log("Hit! + " + collision.gameObject);
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
}