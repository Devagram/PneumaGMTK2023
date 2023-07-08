using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps
{
    public class Trap : MonoBehaviour
    {
        [SerializeField]
        public string currentStateString;
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
        public Collision2D colidedWith;

        void Start()
        {
            if (!possessable && isActive)
            {
                currentState = State.Ready;
            } else
            {
                currentState = State.Deactivated;
            }
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

        void OnCollisionEnter2D(Collision2D col)
        {
            colidedWith = col;
            Debug.Log("Trap collided with");
            if (!possessable && isActive)
            {
                OnTriggered(col);
            }
        }
        void OnCollisionExit2D()
        {
            colidedWith = null;
        }

        public void OnTriggered(Collision2D col)
        {
            if (currentState == State.Ready && isActive)
            {
                TriggerLogic(col.gameObject);
            }
        }
        public IEnumerator TriggerLogic(GameObject triggeringObj){
            currentState = State.Triggered;
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
