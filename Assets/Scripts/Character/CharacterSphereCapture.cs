using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSphereCapture : MonoBehaviour
{
    [NonSerialized] public Collider BlockCollider;
    
    private List<Collider> BlockInSphereCollider = new List<Collider>();
    private List<Coroutine> BlockInSphereCoroutine = new List<Coroutine>();
    
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Block"))
        {
            Rigidbody Rigidbody = Other.GetComponent<Rigidbody>();
            
            Rigidbody.useGravity = false;

            BlockInSphereCollider.Add(Other);
            BlockInSphereCoroutine.Add(StartCoroutine(Capture(Rigidbody)));
        }
    }
    
    private void FixedUpdate()
    {
        Collider BlockInDistance = null;

        foreach (Collider Block in BlockInSphereCollider)
        {
            if (!BlockInDistance ||
                Vector3.Distance(Block.transform.position, transform.position) <
                Vector3.Distance(BlockInDistance.transform.position, transform.position))
            {
                BlockInDistance = Block;
            }
        }

        if (BlockInDistance && BlockCollider != BlockInDistance)
            BlockCollider = BlockInDistance;
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

            if (!(Other != BlockCollider)) BlockCollider = null;
        }
    }
    
    private IEnumerator Capture(Rigidbody Rigidbody)
    {
        float CaptureForce = CharacterContainer.Instance.SphereCaptureForce;
        float CaptureTime = CharacterContainer.Instance.SphereCaptureTime;
        
        if (!BlockInMotion(Rigidbody))
        {
            Rigidbody.AddForce(new Vector3(
                0.0f, 1.0f, 0.0f) * CaptureForce);

            yield return new WaitForSeconds(CaptureTime);
        }
        
        Vector3 Velocity = new Vector3();
        
        while (BlockInMotion(Rigidbody))
        {
            Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, 
                new Vector3(), ref Velocity, CaptureTime);

            yield return new WaitForFixedUpdate();
        }
    }

    private bool BlockInMotion(Rigidbody Rigidbody)
    {
        return Rigidbody.velocity != new Vector3(0.0f, 0.0f, 0.0f);
    }
}
