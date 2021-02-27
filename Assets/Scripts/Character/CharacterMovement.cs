using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [NonSerialized] public Rigidbody Rigidbody;
    [NonSerialized] public Animator Animator;
    
    private bool OnCast;
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }

    public void ChangeState()
    {
        if (OnCast)
        {
            Animator.CrossFade("Movement-Free",
                CharacterContainer.Instance.MoveSmoothTime);

            OnCast = false;
        }
        else
        {
            Animator.CrossFade("Movement-Cast",
                CharacterContainer.Instance.MoveSmoothTime);

            OnCast = true;
        }
    }

    [NonSerialized] public Vector3 Direction;

    public void ChangePosition()
    {
        
    }
    
    private Vector3 DirectionVelocity;
    
    public void ChangeDirection()
    {
        if (Direction != new Vector3(0.0f, 0.0f, 0.0f)) Direction = 
            Vector3.SmoothDamp(Direction, new Vector3(0.0f, 0.0f, 0.0f),
                ref DirectionVelocity, CharacterContainer.Instance.MoveSmoothTime);
    }

    private Vector3 RotationDirection;
    private Vector3 RotationVelocity;

    public void ChangeRotation()
    {
        if (RotationDirection != Direction) RotationDirection = 
            Vector3.SmoothDamp(RotationDirection, Direction,
                ref RotationVelocity, CharacterContainer.Instance.MoveSmoothTime);
        
        transform.rotation = Quaternion.LookRotation(RotationDirection);
    }
    
    private float AnimatorSpeed;
    private float AnimatorVelocity;

    public void ChangeAnimatorValue(float Value)
    {
        if (AnimatorSpeed != Value) AnimatorSpeed = 
            Mathf.SmoothDamp(AnimatorSpeed, Value,
                ref AnimatorVelocity, CharacterContainer.Instance.MoveSmoothTime);
        
        Animator.SetFloat("Speed", AnimatorSpeed);
    }
    
    private float SpeedCurrent;
    private float SpeedVelocity;
    
    public float ChangeSpeed()
    {
        float MoveSpeed = CharacterContainer.Instance.MoveSpeed;
        float MoveSmoothTime = CharacterContainer.Instance.MoveSmoothTime;
        
        if (OnCast && SpeedCurrent > MoveSpeed / 2.0f) SpeedCurrent =
            Mathf.SmoothDamp(SpeedCurrent, MoveSpeed / 2.0f,
                ref SpeedVelocity, MoveSmoothTime);
        
        else if (!OnCast && SpeedCurrent < MoveSpeed) SpeedCurrent =
            Mathf.SmoothDamp(SpeedCurrent, MoveSpeed,
                ref SpeedVelocity, MoveSmoothTime);

        return SpeedCurrent * Time.fixedDeltaTime;
    }
}