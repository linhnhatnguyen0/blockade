using UnityEngine;
using UnityEngine.EventSystems;

public class MouseManager : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // Récupère le GameObject sur lequel vous avez cliqué
        GameObject clickedObject = eventData.pointerCurrentRaycast.gameObject;
        
        // Faites quelque chose avec le GameObject cliqué
        Debug.Log("GameObject UI cliqué : " + clickedObject.name);
    }
}
