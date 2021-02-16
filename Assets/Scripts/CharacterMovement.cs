using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Range(0.0f, 1000.0f)][SerializeField] private float Speed;
    [Range(0.0f, 1.0f)] [SerializeField] private float SmoothTime;
    public Joystick Joystick;
    public Touch Touch;

    private Rigidbody Rigidbody;
    private Animator Animator;

    private Vector3 Direction;
    private float Distance;
    
    private float DirectionVelocityX;
    private float DirectionVelocityZ;
    private float DistanceVelocity;
    private float SpeedVelocity;
    
    private float CurrentSpeed;

    private bool OnTouch;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();

        CurrentSpeed = Speed;
    }

    private void Start()
    {
        Touch.TouchBegin += OnTouchBegin;
        Touch.TouchFinish += OnTouchFinish;
    }

    private void OnTouchBegin()
    {
        Animator.CrossFade("Movement-Cast", SmoothTime);

        OnTouch = true;
    }
    
    private void OnTouchFinish()
    {
        Animator.CrossFade("Movement-Free", SmoothTime);

        OnTouch = false;
    }

    private void FixedUpdate()
    {
        if (OnDrag())
        {
            Direction = new Vector3(Joystick.Direction.x, 0.0f, Joystick.Direction.y);
            Distance = Joystick.Distance;
            
            transform.rotation = Quaternion.LookRotation(Direction);
        }
        else
        {
            SmoothDirection();
            SmoothDistance();
        }
        
        Rigidbody.velocity = Direction * SmoothSpeed();
        Animator.SetFloat("Speed", Distance);
    }

    private bool OnDrag()
    {
        return Joystick.Direction != new Vector2(0.0f, 0.0f) || Joystick.Distance != 0.0f;
    }

    private void SmoothDirection()
    {
        if (Direction.x != 0.0f || Direction.z != 0.0f)
        {
            Direction = new Vector3(
                Mathf.SmoothDamp(Direction.x, 0.0f, ref DirectionVelocityX, SmoothTime), 0.0f, 
                Mathf.SmoothDamp(Direction.z, 0.0f, ref DirectionVelocityZ, SmoothTime));
        }
    }

    private void SmoothDistance()
    {
        if (Distance != 0.0f)
        {
            Distance = Mathf.SmoothDamp(Distance, 0.0f, ref DistanceVelocity, SmoothTime);
        }
    }

    private float SmoothSpeed()
    {
        if (OnTouch && CurrentSpeed != (Speed / 2.0f))
        {
            CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, (Speed / 2.0f), ref SpeedVelocity, SmoothTime);
        }
        else if (!OnTouch && CurrentSpeed != Speed)
        {
            CurrentSpeed = Mathf.SmoothDamp(CurrentSpeed, Speed, ref SpeedVelocity, SmoothTime);
        }

        return CurrentSpeed * Time.fixedDeltaTime;
    }
}
