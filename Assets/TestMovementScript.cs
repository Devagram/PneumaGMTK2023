using UnityEngine;

public class TestMovementScript : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;

    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector2(transform.position.x + movementSpeed, transform.position.y);
        }
    }
}
