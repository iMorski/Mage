using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float TapDistance;
    
    public delegate void OnSwipe(Vector2 Direction);
    public event OnSwipe Swipe;

    public delegate void OnTap();
    public event OnTap Tap;

    private Vector2 BeginMousePosition;

    public void OnPointerDown(PointerEventData Data)
    {
        if (InScreen(Data)) BeginMousePosition = MousePosition(Data);
    }

    private bool OnSwipeFinish;
    public void OnDrag(PointerEventData Data)
    {
        if (!OnSwipeFinish && DragDistance(Data) > TapDistance)
        {
            Vector2 Direction = MousePosition(Data) - BeginMousePosition;
            
            float MaxValue = Math.Max(
                Math.Abs(Direction.x), Math.Abs(Direction.y));
            
            Vector2 DirectionInCircle = Vector2.ClampMagnitude(Direction, MaxValue);
            Vector2 DirectionNormalized = new Vector2(DirectionInCircle.x / MaxValue,
                DirectionInCircle.y / MaxValue);
            
            Swipe?.Invoke(DirectionNormalized);

            OnSwipeFinish = true;
        }
    }

    public void OnPointerUp(PointerEventData Data)
    {
        if (DragDistance(Data) < TapDistance)
        {
            Tap?.Invoke();
        }
        
        OnSwipeFinish = false;
    }

    private float DragDistance(PointerEventData Data)
    {
        return Vector2.Distance(MousePosition(Data), BeginMousePosition);
    }
    
    private bool InScreen(PointerEventData Data)
    {
        return Data.hovered.Contains(gameObject);
    }

    private Vector2 MousePosition(PointerEventData Data)
    {
        return Data.position;
    }
}