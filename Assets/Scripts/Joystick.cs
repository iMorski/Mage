using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public static Vector2 Direction;
    public static float Distance;

    private GameObject HandleArea;
    private GameObject Handle;

    private Image HandleAreaImage;
    private Image HandleImage;
    
    private float HandleAreaRadius;

    private Vector2 BeginMousePosition;

    private void Awake()
    {
        HandleArea = transform.GetChild(0).gameObject;
        Handle = transform.GetChild(1).gameObject;

        HandleAreaImage = HandleArea.GetComponent<Image>();
        HandleImage = Handle.GetComponent<Image>();
    }

    private void Start()
    {
        Rect HandleAreaRect = HandleArea.GetComponent<RectTransform>().rect;
        
        HandleAreaRadius = Math.Max(HandleAreaRect.width, HandleAreaRect.height) / 2.0f *
                           GetComponentInParent<Canvas>().scaleFactor;
    }

    public void OnPointerDown(PointerEventData Data)
    {
        if (!InScreen(Data)) return;
        
        BeginMousePosition = MousePosition(Data);

        HandleArea.transform.position = BeginMousePosition;
        Handle.transform.position = BeginMousePosition;

        HandleAreaImage.enabled = true;
        HandleImage.enabled = true;
    }

    public void OnDrag(PointerEventData Data)
    {
        Vector2 DirectionInPixel = MousePosition(Data) - BeginMousePosition;
        Vector2 DirectionInPixelInCircle = Vector2.ClampMagnitude(DirectionInPixel, HandleAreaRadius);

        float DragDistance = Vector2.Distance(MousePosition(Data), BeginMousePosition);

        if (DragDistance > HandleAreaRadius ||
            DragDistance > HandleAreaRadius)
        {
            BeginMousePosition = BeginMousePosition + (DirectionInPixel - DirectionInPixelInCircle);
        }
        
        HandleArea.transform.position = BeginMousePosition;
        Handle.transform.position = MousePosition(Data);
        
        Direction = new Vector2(DirectionInPixelInCircle.x / HandleAreaRadius,
            DirectionInPixelInCircle.y / HandleAreaRadius);
        Distance = Vector2.Distance(new Vector2(0.0f, 0.0f), 
                       DirectionInPixelInCircle) / HandleAreaRadius;
    }

    public void OnPointerUp(PointerEventData Data)
    {
        HandleAreaImage.enabled = false;
        HandleImage.enabled = false;
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