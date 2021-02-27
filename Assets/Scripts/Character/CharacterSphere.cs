using System;
using System.Collections;
using UnityEngine;

public class CharacterSphere : MonoBehaviour
{
    [SerializeField] private Transform SphereMesh;

    public delegate void OnChangeCast(bool OnCast);
    public event OnChangeCast ChangeCast;
    
    [NonSerialized] public SphereCollider SphereCollider;
    [NonSerialized] public bool OnCast;

    public void ChangeState()
    {
        StopAllCoroutines();

        if (!OnCast)
        {
            StartCoroutine(ChangeScale(CharacterContainer.Instance.SphereRadius));

            OnCast = true;
        }
        else
        {
            StartCoroutine(ChangeScale(0.0f));

            OnCast = false;
        }
        
        ChangeCast?.Invoke(OnCast);
    }
    
    private float RadiusVelocity;
        
    private Vector3 MeshVelocity;
    private Vector3 ScaleOnRadius;
    
    private IEnumerator ChangeScale(float Scale)
    {
        ScaleOnRadius = new Vector3(
            Scale * 2.0f, Scale * 2.0f, Scale * 2.0f);
        
        while (SphereCollider.radius != Scale ||
               SphereMesh.localScale != ScaleOnRadius)
        {
            SphereCollider.radius = 
                Mathf.SmoothDamp(SphereCollider.radius, Scale,
                    ref RadiusVelocity, CharacterContainer.Instance.SphereSmoothTime);

            SphereMesh.localScale = 
                Vector3.SmoothDamp(SphereMesh.localScale, ScaleOnRadius,
                    ref MeshVelocity, CharacterContainer.Instance.SphereSmoothTime);
            
            yield return new WaitForFixedUpdate();
        }
    }
}
