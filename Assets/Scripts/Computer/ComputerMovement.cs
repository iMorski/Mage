using System.Collections;
using UnityEngine;

public class ComputerMovement : CharacterMovement
{
    private bool OnWait;
    private bool OnWaitCoroutine;
    
    private void FixedUpdate()
    {
        GameObject Block = ComputerContainer.Instance.GetBlock(transform);
        
        float DistanceToBlock = Vector3.Distance(Block.transform.position, transform.position);
        float DistanceToStop = ComputerContainer.Instance.DistanceToStop;

        if (DistanceToBlock > DistanceToStop)
        {
            if (OnWait && !OnWaitCoroutine)
            {
                StartCoroutine(Wait());
            }
            else if (!OnWait)
            {
                Vector3 BlockPosition = Block.transform.position;
                Vector3 Position = transform.position;

                Direction = Vector3.Normalize(new Vector3(BlockPosition.x - Position.x, 0.0f,
                    BlockPosition.z - Position.z));

                ChangeRotation();
                ChangeAnimatorValue(1.0f);
            }
        }
        else
        {
            ChangeDirection();
            ChangeAnimatorValue(0.0f);

            OnWait = true;
        }
        
        Rigidbody.velocity = Direction * ChangeSpeed();
    }

    private IEnumerator Wait()
    {
        OnWaitCoroutine = !OnWaitCoroutine;
        
        yield return new WaitForSeconds(ComputerContainer.Instance.Wait());

        OnWait = !OnWait;
        OnWaitCoroutine = !OnWaitCoroutine;
    }
}
