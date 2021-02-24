using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }
    
    private Joystick Joystick;

    private void Start()
    {
        Joystick = PlayerContainer.Instance.Joystick;
        
        PlayerContainer.Instance.Touch.Tap += OnTap;
        PlayerContainer.Instance.Touch.Swipe += OnSwipe;
    }

    private void OnTap() { ChangeState(); }
    private void OnSwipe(Vector2 Direction) { ChangeState(); }
    
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
        
        Rigidbody.velocity = Direction * SmoothSpeed();
    }
}
