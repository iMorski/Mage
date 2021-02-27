using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private Joystick Joystick;

    private void Start()
    {
        Joystick = PlayerContainer.Instance.Joystick;
        
        PlayerContainer.Instance.Touch.TouchBegin += OnTouchBegin;
        PlayerContainer.Instance.Touch.TouchFinish += OnTouchFinish;
    }

    private void OnTouchBegin() { ChangeState(); }
    private void OnTouchFinish() { ChangeState(); }

    private void FixedUpdate()
    {
        if (Joystick.Direction != new Vector2(0.0f, 0.0f))
        {
            Direction = Vector3.Normalize(new Vector3(
                Joystick.Direction.x, 0.0f, Joystick.Direction.y));

            ChangeRotation();
            ChangeAnimatorValue(1.0f);
        }
        else
        {
            ChangeDirection();
            ChangeAnimatorValue(0.0f);
        }

        Rigidbody.velocity = Direction * ChangeSpeed();
    }
}
