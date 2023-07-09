using UnityEngine;

namespace Traps
{
    public class SpringTrap : Trap
    {
        [SerializeField]
        private float force;

        public override void TrapTriggerBehavior(GameObject triggeringObj)
        {
            Rigidbody2D rb;

            //Doesn't affect the playable ghost character
            if (triggeringObj.TryGetComponent<Rigidbody2D>(out rb))
            {
                Vector3 dir = triggeringObj.transform.position - transform.position;

                //Move colliding object in opposite direction of collision
                rb.AddForce(dir * force);
            }
        }
    }
}
