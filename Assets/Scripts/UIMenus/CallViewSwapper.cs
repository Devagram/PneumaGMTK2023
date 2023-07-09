using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallViewSwapper : MonoBehaviour
{
    [SerializeField]
    public GameObject UIMenus;
    [SerializeField]
    public GameObject enableObject;
    [SerializeField]
    public GameObject disableObject;

    private ViewSwapper viewSwapper;


    // Start is called before the first frame update
    private void Start()
    {
        viewSwapper = UIMenus.GetComponent<ViewSwapper>();
    }
    public void SwapView()
    {
        viewSwapper.DisableView(disableObject);
        viewSwapper.EnableView(enableObject);
    }
}
