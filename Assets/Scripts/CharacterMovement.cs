using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float SmoothTime;
    
    private Joystick Joystick;

    private Rigidbody Rigidbody;
    private Animator Animator;

    private float CurrentSpeed;
    private void Awake()
    {
        Joystick = GetComponent<CharacterContainer>().Joystick;
        
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();

        CurrentSpeed = Speed;
    }

    private void Start()
    {
        CharacterContainer CharacterContainer =
            GetComponentInParent<CharacterContainer>();
        
        CharacterContainer.Touch.Tap += OnTap;
        CharacterContainer.Touch.Swipe += OnSwipe;
    }
    
    private void OnTap() { Resolve(); }
    private void OnSwipe(Vector2 Direction) { Resolve(); }

    private bool OnCast;

    private void Resolve()
    {
        if (!OnCast)
        {
            Animator.CrossFade("Movement-Cast", SmoothTime);

            OnCast = true;
        }
        else
        {
            Animator.CrossFade("Movement-Free", SmoothTime);

            OnCast = false;
        }
    }
    
    private Vector3 Direction;

    private float AnimatorSpeed;
    private float AnimatorVelocity;
    
    private void FixedUpdate()
    {
        if (OnDrag())
        {
            Direction = Vector3.Normalize(new Vector3(
                Joystick.Direction.x, 0.0f, Joystick.Direction.y));

            if (AnimatorSpeed < 1.0f)
            {
                AnimatorSpeed = Mathf.SmoothDamp(AnimatorSpeed, 1.0f,
                    ref AnimatorVelocity, SmoothTime);
                
                Animator.SetFloat("Speed", AnimatorSpeed);
            }
            
            transform.rotation = Quaternion.LookRotation(Direction);
        }
        else
        {
            SmoothDirection();

            if (AnimatorSpeed > 0.0f)
            {
                AnimatorSpeed = Mathf.SmoothDamp(AnimatorSpeed, 0.0f,
                    ref AnimatorVelocity, SmoothTime);
                
                Animator.SetFloat("Speed", AnimatorSpeed);
            }
        }
        
        Rigidbody.velocity = Direction * SmoothSpeed();
    }

    private bool OnDrag()
    {
        return Joystick.Direction != new Vector2(0.0f, 0.0f) ||
               Joystick.Distance != 0.0f;
    }

    private float DirectionVelocityX;
    private float DirectionVelocityZ;
    
    private void SmoothDirection()
    {
        if (Direction.x != 0.0f) Direction.x =
            Mathf.SmoothDamp(Direction.x, 0.0f, ref DirectionVelocityX, SmoothTime);
        
        if (Direction.z != 0.0f) Direction.z =
            Mathf.SmoothDamp(Direction.z, 0.0f, ref DirectionVelocityZ, SmoothTime);

        Direction = new Vector3(Direction.x, 0.0f, Direction.z);
    }

    private float SpeedVelocity;
    
    private float SmoothSpeed()
    {
        if (OnCast && CurrentSpeed > Speed / 2.0f) CurrentSpeed =
            Mathf.SmoothDamp(CurrentSpeed, Speed / 2.0f, ref SpeedVelocity, SmoothTime);
        
        else if (!OnCast && CurrentSpeed < Speed) CurrentSpeed =
            Mathf.SmoothDamp(CurrentSpeed, Speed, ref SpeedVelocity, SmoothTime);

        return CurrentSpeed * Time.fixedDeltaTime;
    }
}