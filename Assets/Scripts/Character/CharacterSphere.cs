using System;
using System.Collections;
using UnityEngine;

public class CharacterSphere : MonoBehaviour
{
    [SerializeField] private Transform Sphere;
    [SerializeField] private Transform SphereMesh;

    [NonSerialized] public bool OnCast;

    public void ChangeState()
    {
        StopAllCoroutines();

        if (!OnCast)
        {
            StartCoroutine(ChangeScale(
                CharacterContainer.Instance.SphereRadius));
        }
        else
        {
            StartCoroutine(ChangeScale(
                0.0f));
        }

        OnCast = !OnCast;
    }
    
    private Vector3 SphereVelocity;
    private Vector3 SphereMeshVelocity;
    
    private IEnumerator ChangeScale(float Value)
    {
        Vector3 Scale = new Vector3(Value, Value, Value);
        
        while (Sphere.localScale != Scale ||
               SphereMesh.localScale != Scale)
        {
            Sphere.localScale = Vector3.SmoothDamp(Sphere.localScale, Scale,
                    ref SphereVelocity, CharacterContainer.Instance.SphereTime);

            SphereMesh.localScale = Vector3.SmoothDamp(SphereMesh.localScale, Scale,
                    ref SphereMeshVelocity, CharacterContainer.Instance.SphereTime);
            
            yield return new WaitForFixedUpdate();
        }
    }
}
