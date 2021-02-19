using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Range(0.0f, 1000.0f)][SerializeField] private float Speed;
    [Range(0.0f, 1.0f)] [SerializeField] private float SmoothTime;
    
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

    private bool OnSphere;
    private void OnTap() { Resolve(); }
    private void OnSwipe(Vector2 Direction)  { Resolve(); }

    private void Resolve()
    {
        if (!OnSphere)
        {
            Animator.CrossFade("Movement-Cast", SmoothTime);

            OnSphere = true;
        }
        else
        {
            Animator.CrossFade("Movement-Free", SmoothTime);

            OnSphere = false;
        }
    }
    
    private Vector3 Direction;
    private float Distance;
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
    
    private float DistanceVelocity;
    private void SmoothDistance()
    {
        if (Distance > 0.0f) Distance =
            Mathf.SmoothDamp(Distance, 0.0f, ref DistanceVelocity, SmoothTime);
    }

    private float SpeedVelocity;
    private float SmoothSpeed()
    {
        if (OnSphere && CurrentSpeed > Speed / 2.0f) CurrentSpeed =
            Mathf.SmoothDamp(CurrentSpeed, Speed / 2.0f, ref SpeedVelocity, SmoothTime);
        
        else if (!OnSphere && CurrentSpeed < Speed) CurrentSpeed =
            Mathf.SmoothDamp(CurrentSpeed, Speed, ref SpeedVelocity, SmoothTime);

        return CurrentSpeed * Time.fixedDeltaTime;
    }
}