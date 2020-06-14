using UnityEngine;
using UnityEngine.EventSystems;

public class Restart : MonoBehaviour, IPointerDownHandler
{
    [System.Obsolete]
    public void OnPointerDown(PointerEventData eventData)
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
