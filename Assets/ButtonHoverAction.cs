using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverAction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    public GameObject helpPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the help panel
        Debug.Log("");
        helpPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the help panel
        helpPanel.SetActive(false);
    }
}
