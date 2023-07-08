using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Traps {
    public class MouthTrap : Trap
    {
        public override void TrapTriggerBehavior(GameObject triggeringObj)
        {
            //kill hero
            triggeringObj.SetActive(false);
        }
    }
}
