using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSwapper : MonoBehaviour
{

    public void DisableView(GameObject viewToDisable)
    {
        viewToDisable.SetActive(false);
    }

    public void EnableView(GameObject viewToEnable)
    {
        viewToEnable.SetActive(true);
    }
}
