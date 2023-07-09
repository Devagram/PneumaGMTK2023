using UnityEngine;

namespace Traps
{
    public class ArrowTrap : Trap
    {
        [SerializeField]
        GameObject arrowPrefab;
        [SerializeField]
        private float arrowOffset;

        public override void TrapFireBehavior(GameObject firingObj)
        {
            Instantiate(arrowPrefab, transform.position + (transform.up*arrowOffset), transform.rotation);
        }
    }
}
