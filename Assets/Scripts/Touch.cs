using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public delegate void OnTouchBegin();
    public event OnTouchBegin TouchBegin;
    
    public delegate void OnTouchFinish();
    public event OnTouchFinish TouchFinish;
    
    public void OnPointerDown(PointerEventData Data)
    {
        if (InScreen(Data)) TouchBegin?.Invoke();
    }

    public void OnDrag(PointerEventData Data)
    {
        
    }

    public void OnPointerUp(PointerEventData Data)
    {
        TouchFinish?.Invoke();
    }
    
    private bool InScreen(PointerEventData Data)
    {
        return Data.hovered.Contains(gameObject);
    }
}
