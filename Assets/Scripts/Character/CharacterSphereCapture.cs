using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSphereCapture : MonoBehaviour
{
    [NonSerialized] public Collider BlockOnSelect;
    
    [NonSerialized] public List<Collider> BlockInSphereCollider = new List<Collider>();
    [NonSerialized] public List<Coroutine> BlockInSphereCoroutine = new List<Coroutine>();
    
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

        if (BlockInDistance && BlockInDistance != BlockOnSelect)
            BlockOnSelect = BlockInDistance;
    }

    private void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag("Block"))
        {
            Rigidbody Rigidbody = Other.GetComponent<Rigidbody>();
            Coroutine Coroutine = BlockInSphereCoroutine[
                BlockInSphereCollider.IndexOf(Other)];

            Rigidbody.useGravity = true;
            
            if (Coroutine != null) StopCoroutine(Coroutine);

            BlockInSphereCollider.Remove(Other);
            BlockInSphereCoroutine.Remove(Coroutine);

            if (!(Other != BlockOnSelect)) BlockOnSelect = null;
        }
    }
    
    private IEnumerator Capture(Rigidbody Rigidbody)
    {
        if (BlockOnGround(Rigidbody))
        {
            Rigidbody.AddForce(new Vector3(
                0.0f, 1.0f, 0.0f) * CharacterContainer.Instance.SphereCaptureRiseForce);

            yield return new WaitForSeconds(CharacterContainer.Instance.SphereCaptureRiseTime);
        }
        
        Vector3 Velocity = new Vector3();
        
        while (BlockInMotion(Rigidbody))
        {
            Rigidbody.velocity = Vector3.SmoothDamp(Rigidbody.velocity, 
                new Vector3(), ref Velocity, CharacterContainer.Instance.SphereCaptureTime);

            yield return new WaitForFixedUpdate();
        }
    }

    private bool BlockOnGround(Rigidbody Rigidbody)
    {
        float GroundDistance = 0.0f;
        
        if (Physics.Raycast(Rigidbody.transform.position, new Vector3(
            0.0f, -1.0f, 0.0f), out RaycastHit Hit)) GroundDistance = Hit.distance;
        
        return GroundDistance < CharacterContainer.Instance.SphereCaptureRiseDistance;
    }

    private bool BlockInMotion(Rigidbody Rigidbody)
    {
        return Rigidbody.velocity != new Vector3(0.0f, 0.0f, 0.0f);
    }
}
