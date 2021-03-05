using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private float TapDistance;
    [SerializeField] private float DoubleTapTime;
    
    public delegate void OnTapBegin();
    public event OnTapBegin TapBegin;
    
    public delegate void OnTap();
    public event OnTap Tap;
    
    public delegate void OnTapFinish();
    public event OnTapFinish TapFinish;
    
    public delegate void OnDoubleTapBegin();
    public event OnDoubleTapBegin DoubleTapBegin;
    
    public delegate void OnDoubleTap();
    public event OnDoubleTap DoubleTap;
    
    public delegate void OnDoubleTapFinish();
    public event OnDoubleTapFinish DoubleTapFinish;
    
    public delegate void OnSwipeByDistance(Vector2 Direction);
    public event OnSwipeByDistance SwipeByDistance;
    
    public delegate void OnSwipeByRelease(Vector2 Direction);
    public event OnSwipeByRelease SwipeByRelease;

    private Vector2 BeginMousePosition;

    private bool TapCheck;
    
    private bool DoubleTapCheck;
    private bool DoubleTapCheckDone;

    public void OnPointerDown(PointerEventData Data)
    {
        if (!InScreen(Data)) return;

        TapCheck = !TapCheck;
        
        TapBegin?.Invoke();
            
        BeginMousePosition = MousePosition(Data);

        if (DoubleTapCheck)
        {
            DoubleTapCheck = !DoubleTapCheck;
            DoubleTapCheckDone = !DoubleTapCheckDone;
            
            DoubleTapBegin?.Invoke();
        }
    }

    private bool OnSwipeCheck;
    
    public void OnDrag(PointerEventData Data)
    {
        if (!OnSwipeCheck && !DragInDistance(Data))
        {
            OnSwipeCheck = !OnSwipeCheck;
            
            SwipeByDistance?.Invoke(GetDirection(Data));
        }
    }

    public void OnPointerUp(PointerEventData Data)
    {
        if (TapCheck)
        {
            TapCheck = !TapCheck;
            
            TapFinish?.Invoke();

            if (DragInDistance(Data))
            {
                Tap?.Invoke();
            }
        }

        if (DoubleTapCheckDone)
        {
            DoubleTapCheckDone = !DoubleTapCheckDone;
            
            DoubleTapFinish?.Invoke();

            if (DragInDistance(Data))
            {
                DoubleTap?.Invoke();
            }
        }
        else if (!DoubleTapCheck)
        {
            DoubleTapCheck = !DoubleTapCheck;
            
            StartCoroutine(DoubleTapCheckTime());
        }
        
        if (!DragInDistance(Data)) SwipeByRelease?.Invoke(GetDirection(Data));
        if (OnSwipeCheck) OnSwipeCheck = !OnSwipeCheck;
    }

    private IEnumerator DoubleTapCheckTime()
    {
        yield return new WaitForSeconds(DoubleTapTime);
        
        if (DoubleTapCheck) DoubleTapCheck = !DoubleTapCheck;
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

    private bool DragInDistance(PointerEventData Data)
    {
        return Vector2.Distance(MousePosition(Data), BeginMousePosition) < TapDistance;
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