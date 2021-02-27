using UnityEngine;

public class ComputerMovement : CharacterMovement
{
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();
    }
    
    private void FixedUpdate()
    {
        GameObject Block = ComputerContainer.Instance.GetBlock(transform);
        
        Vector3 BlockPosition = Block.transform.position;
        Vector3 Position = transform.position;
        
        if (Vector3.Distance(BlockPosition, Position) > ComputerContainer.Instance.Distance)
        {
            Direction = Vector3.Normalize(new Vector3(BlockPosition.x - Position.x, 0.0f, 
                BlockPosition.z - Position.z));

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
