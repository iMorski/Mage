using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSphereCapture : MonoBehaviour
{
    [SerializeField] private float ForceMultiplier;
    [SerializeField] private float CaptureTime;

    private List<Collider> BlockInSphereCollider = new List<Collider>();
    private List<Coroutine> BlockInSphereCoroutine = new List<Coroutine>();
    
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Block"))
        {
            Rigidbody Rigidbody = Other.GetComponent<Rigidbody>();
            
            Rigidbody.useGravity = false;

            BlockInSphereCollider.Add(Other);
            BlockInSphereCoroutine.Add(
                StartCoroutine(Capture(Rigidbody)));
        }
    }

    [NonSerialized]
    public Rigidbody PushRigidbody;

    private void FixedUpdate()
    {
        Collider MinBlock = null;

        foreach (Collider Block in BlockInSphereCollider)
        {
            if (!MinBlock) MinBlock = Block;

            float MinBlockDistance = Vector3.Distance(MinBlock.transform.position, transform.position);
            float BlockDistance = Vector3.Distance(Block.transform.position, transform.position);

            if (BlockDistance < MinBlockDistance) MinBlock = Block;
        }

        if (MinBlock && PushRigidbody != MinBlock.GetComponent<Rigidbody>())
            PushRigidbody = MinBlock.GetComponent<Rigidbody>();
    }

    private void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag("Block"))
        {
            Rigidbody Rigidbody = Other.GetComponent<Rigidbody>();
            Coroutine Coroutine = BlockInSphereCoroutine[
                BlockInSphereCollider.IndexOf(Other)];

            Rigidbody.useGravity = true;
            
            StopCoroutine(Coroutine);

            BlockInSphereCollider.Remove(Other);
            BlockInSphereCoroutine.Remove(Coroutine);
        }
    }
    
    private IEnumerator Capture(Rigidbody Rigidbody)
    {
        if (!(Rigidbody.velocity.x != 0.0f) ||
            !(Rigidbody.velocity.y != 0.0f) ||
            !(Rigidbody.velocity.z != 0.0f))
        {
            Rigidbody.AddForce(new Vector3(0.0f, 1.0f, 0.0f) * ForceMultiplier);

            yield return new WaitForSeconds(CaptureTime);
        }

        float VelocityValueX = Rigidbody.velocity.x;
        float VelocityValueY = Rigidbody.velocity.y;
        float VelocityValueZ = Rigidbody.velocity.z;
        
        float VelocityX = 0.0f;
        float VelocityY = 0.0f;
        float VelocityZ = 0.0f;
        
        while (VelocityValueX != 0.0f ||
               VelocityValueY != 0.0f ||
               VelocityValueZ != 0.0f)
        {
            if (VelocityValueX != 0.0f) VelocityValueX =
                Mathf.SmoothDamp(VelocityValueX, 0.0f, ref VelocityX, CaptureTime);
            
            if (VelocityValueY != 0.0f) VelocityValueY =
                Mathf.SmoothDamp(VelocityValueY, 0.0f, ref VelocityY, CaptureTime);
            
            if (VelocityValueZ != 0.0f) VelocityValueZ =
                Mathf.SmoothDamp(VelocityValueZ, 0.0f, ref VelocityZ, CaptureTime);

            Rigidbody.velocity = new Vector3(VelocityValueX, VelocityValueY, VelocityValueZ);

            yield return new WaitForFixedUpdate();
        }
    }
}
