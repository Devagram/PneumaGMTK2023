using UnityEngine;

namespace Traps
{
    public class SpringTrap : Trap
    {
        [SerializeField]
        private float force;

        private Coroutine waitCoroutine;

        public override void TrapTriggerBehavior(GameObject triggeringObj)
        {
            Rigidbody2D rb;

            //Doesn't affect the playable ghost character
            if (triggeringObj.TryGetComponent<Rigidbody2D>(out rb))
            {
                Debug.Log("BOING");
                Vector3 dir = triggeringObj.transform.position - transform.position;
                if (triggeringObj.tag == "Hero")
                {
                    Debug.Log("BOINGED THE HERO");
                    NewHeroWanderScript heroScript = triggeringObj.gameObject.GetComponentInChildren<NewHeroWanderScript>();
                    waitCoroutine = StartCoroutine(heroScript.Wait());
                }
                Debug.Log("APPLYING FORCE = " + dir * 5 * force);
                //Move colliding object in opposite direction of collision
                rb.AddForce(dir * 5 * force);
            }
        }
    }
}
