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
            if (possessable && possessed && isActive)
            {
                currentState = State.Ready;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //FireTrap();
                }
            } if (!possessable && isActive)
            {
                currentState = State.Ready;
            }
            
        }

        public IEnumerator Cooldown()
        {
            yield return new WaitForSeconds(cooldownTime);
            isActive = true;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            collidedWith = col;
            Debug.Log("Trap collided with");
            if (!possessable && isActive)
            {
                OnTriggered(col);
            }
        }
        void OnCollisionExit2D()
        {
            collidedWith = null;
        }

        public void OnTriggered(Collider2D col)
        {
            if (currentState == State.Ready && isActive)
            {
                StartCoroutine(TriggerLogic(col.gameObject));
            }
        }
        public IEnumerator TriggerLogic(GameObject triggeringObj){
            currentState = State.Triggered;
            trapAnimator.PlayTrapAnimation();
            TrapTriggerBehavior(triggeringObj);
            isActive = false;
            yield return new WaitForSeconds(cooldownTime);
            Cooldown();
        }
        /*public void OnFired()
        {
            Debug.Log("Trap fired manually");
            if (currentState == State.Ready && isActive)
            {
                FiredLogic();
            }
        }*/
        /*public IEnumerator FiredLogic()
        {
            currentState = State.Triggered;
            TrapFireBehavior(colidedWith.gameObject);
            isActive = false;
            yield return new WaitForSeconds(cooldownTime);
            Cooldown();
        }*/
        public virtual void TrapTriggerBehavior(GameObject triggeringObj) {}
        //public virtual void TrapFireBehavior(GameObject objToHit) {}

        /*public void FireTrap()
        {
            if (colidedWith != null)
            {
                OnFired();
            }
            else
            {
                 //miss
            }
        }*/
    }
}
