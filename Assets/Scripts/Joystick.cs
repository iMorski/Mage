using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private Vector2 ScreenAreaInPercentX;
    [SerializeField] private Vector2 ScreenAreaInPercentY;
    [Range(0.0f, 1.0f)][SerializeField] private float DragInPercent;

    public static Vector2 Direction;
    public static float Distance;

    private Vector2 BeginMousePosition;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && InScreenArea())
        {
            BeginMousePosition = MousePosition();
        }
        else if (Input.GetMouseButton(0))
        {
            Vector2 DirectionInPixel = -1 * (BeginMousePosition - MousePosition());
            Vector2 DirectionInPixelInCircle = Vector2.ClampMagnitude(DirectionInPixel,
                DragInPercent * Height());

            float DragInPixel = DragInPercent * Height();
            float DistanceInPixel = Vector2.Distance(new Vector2(0.0f, 0.0f), DirectionInPixelInCircle);

            Direction = new Vector2(DirectionInPixelInCircle.x / DragInPixel,
                DirectionInPixelInCircle.y / DragInPixel);
            Distance = DistanceInPixel / DragInPixel;
        }
    }

    private bool InScreenArea()
    {
        Vector2 ScreenAreaInPixelX = ScreenAreaInPercentX * Width();
        Vector2 ScreenAreaInPixelY = ScreenAreaInPercentY * Height();
        
        return MousePosition().x > ScreenAreaInPixelX.x &&
               MousePosition().x < ScreenAreaInPixelX.y &&
               MousePosition().y > ScreenAreaInPixelY.x &&
               MousePosition().y < ScreenAreaInPixelY.y;
    }

    private Vector2 MousePosition()
    {
        return Input.mousePosition;
    }

    private float Width()
    {
        return Screen.width;
    }
    
    private float Height()
    {
        return Screen.height;
    }
}
