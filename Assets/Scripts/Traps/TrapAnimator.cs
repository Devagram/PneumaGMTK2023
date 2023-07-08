using UnityEngine;

public class TrapAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PlayTrapAnimation();
        }
    }

    public void PlayTrapAnimation()
    {
        animator.SetTrigger("Triggered");
    }
}
