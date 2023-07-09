using System.Collections;
using UnityEngine;

namespace Traps
{
    [RequireComponent(typeof(TrapAnimator))]
    public class Trap : MonoBehaviour
    {
        [SerializeField]
        public string currentStateString;
        public TrapAnimator trapAnimator;
        public enum State
        {
            Ready,
            Triggered,
            Deactivated
        }

        private State currentState;
        [SerializeField]
        public float triggerDuration = 0f;
        [SerializeField]
        public float cooldownTime = 0f;
        [SerializeField]
        public bool isActive = true;
        [SerializeField]
        public bool possessable = false;
        [SerializeField]
        public bool possessed = false;
        [SerializeField]
        public Collider2D collidedWith;

        void Start()
        {
            if (!possessable && isActive)
            {
                currentState = State.Ready;
            } else
            {
                currentState = State.Deactivated;
            }

            trapAnimator = GetComponent<TrapAnimator>();
        }

        private void Update()
        {
            currentStateString = currentState.ToString();
            if (isActive)
            {
                if (!possessable)
                {
                    currentState = State.Ready;
                }
                else
                {
                    if (collidedWith != null && Input.GetKeyDown(KeyCode.E))
                    {
                        possessed = true;
                    }

                    if (possessed)
                    {
                        currentState = State.Ready;
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            OnFired();
                        }
                    }
                }
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            collidedWith = col;
            Debug.Log("Trap collided with");
            //Non-possessable traps don't affect the playable ghost
            if (!possessable && isActive && !col.CompareTag("Ghost"))
            {
                OnTriggered();
            }
        }
        void OnTriggerExit2D()
        {
            collidedWith = null;
            possessed = false;
        }

        public void OnTriggered()
        {
            Debug.Log("Trap triggered");
            if (currentState == State.Ready && isActive)
            {
                TriggerLogic();
            }
        }
        public void TriggerLogic(){
            currentState = State.Triggered;
            trapAnimator.PlayTrapAnimation();
            TrapTriggerBehavior(collidedWith.gameObject);
            isActive = false;
            StartCoroutine(Cooldown());
        }

        public void OnFired()
        {
            Debug.Log("Trap fired manually");
            if (currentState == State.Ready && isActive)
            {
                FiredLogic();
            }
        }
        public void FiredLogic()
        {
            currentState = State.Triggered;
            trapAnimator.PlayTrapAnimation();
            TrapFireBehavior(collidedWith.gameObject);
            isActive = false;
            StartCoroutine(Cooldown());
        }

        private IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(cooldownTime);
            isActive = true;
            currentState = State.Ready;
        }

        public virtual void TrapTriggerBehavior(GameObject triggeringObj) {}
        public virtual void TrapFireBehavior(GameObject firingObj) {}
    }
}
