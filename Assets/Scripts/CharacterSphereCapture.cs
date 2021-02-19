using System;
using System.Collections;
using UnityEngine;

public class CharacterSphereCapture : MonoBehaviour
{
    [SerializeField] private float ForceMultiplier;
    [SerializeField] private float CaptureTime;

    [NonSerialized] public Rigidbody Rigidbody;
    
    private void OnTriggerEnter(Collider Other)
    {
        if (Other.CompareTag("Block"))
        {
            Rigidbody = Other.GetComponent<Rigidbody>();
            Rigidbody.useGravity = false;
            
            StartCoroutine(Capture());
        }
    }

    private void OnTriggerExit(Collider Other)
    {
        if (Other.CompareTag("Block"))
        {
            StopAllCoroutines();
        
            Rigidbody.useGravity = true;
            Rigidbody = null;
        }
    }
    
    private IEnumerator Capture()
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
