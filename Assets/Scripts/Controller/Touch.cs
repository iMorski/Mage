using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float TapDistance;
    
    public delegate void OnTouchBegin();
    public event OnTouchBegin TouchBegin;

    public delegate void OnTap();
    public event OnTap Tap;
    
    public delegate void OnSwipe(Vector2 Direction);
    public event OnSwipe Swipe;
    
    public delegate void OnSwipeRelease(Vector2 Direction);
    public event OnSwipeRelease SwipeRelease;
    
    public delegate void OnTouchFinish();
    public event OnTouchFinish TouchFinish;

    private Vector2 BeginMousePosition;

    public void OnPointerDown(PointerEventData Data)
    {
        if (!InScreen(Data)) return;
        
        TouchBegin?.Invoke();
            
        BeginMousePosition = MousePosition(Data);
    }

    private bool OnSwipeFinish;
    
    public void OnDrag(PointerEventData Data)
    {
        if (!OnSwipeFinish && DragDistance(Data) > TapDistance)
        {
            Swipe?.Invoke(GetDirection(Data));

            OnSwipeFinish = true;
        }
    }

    public void OnPointerUp(PointerEventData Data)
    {
        TouchFinish?.Invoke();
        
        if (DragDistance(Data) < TapDistance) Tap?.Invoke();
        else SwipeRelease?.Invoke(GetDirection(Data));
        
        OnSwipeFinish = false;
    }

    private Vector2 GetDirection(PointerEventData Data)
    {
        Vector2 Direction = MousePosition(Data) - BeginMousePosition;
            
        float MaxValue = Math.Max(
            Math.Abs(Direction.x), Math.Abs(Direction.y));
            
        Vector2 DirectionInCircle = Vector2.ClampMagnitude(Direction, MaxValue);
        Vector2 DirectionNormalized = new Vector2(DirectionInCircle.x / MaxValue,
            DirectionInCircle.y / MaxValue);

        return DirectionNormalized;
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