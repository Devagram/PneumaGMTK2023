using UnityEngine;

namespace Traps
{
    public class MouthTrap : Trap
    {
        public override void TrapTriggerBehavior(GameObject triggeringObj)
        {
            //Doesn't kill the playable ghost character
            if (!triggeringObj.gameObject.tag.Equals("Ghost"))
            {
                //kill hero
                triggeringObj.SetActive(false);
            }
        }
    }
}
