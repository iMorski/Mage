using System.Collections;
using UnityEngine;

public class CharacterSphere : MonoBehaviour
{
    [SerializeField] private float Radius;
    [SerializeField] private float RadiusSmoothTime;
    [SerializeField] private Transform SphereMesh;
    
    private SphereCollider Collider;
    
    private bool OnSphere;

    private void Awake()
    {
        Collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        CharacterContainer CharacterContainer =
            GetComponentInParent<CharacterContainer>();
        
        CharacterContainer.Touch.Tap += OnTap;
        CharacterContainer.Touch.Swipe += OnSwipe;
    }

    private void OnTap() { Resolve(); }
    private void OnSwipe(Vector2 Direction)  { Resolve(); }

    private void Resolve()
    {
        StopAllCoroutines();

        if (!OnSphere)
        {
            StartCoroutine(SphereEnable());

            OnSphere = true;
        }
        else
        {
            StartCoroutine(SphereDisable());

            OnSphere = false;
        }
    }

    private IEnumerator SphereEnable()
    {
        float RadiusVelocity = 0.0f;
        
        float MeshVelocityValueX = SphereMesh.localScale.x;
        float MeshVelocityValueY = SphereMesh.localScale.y;
        float MeshVelocityValueZ = SphereMesh.localScale.z;
        
        float MeshVelocityX = 0.0f;
        float MeshVelocityY = 0.0f;
        float MeshVelocityZ = 0.0f;
        
        while (Collider.radius < Radius)
        {
            Collider.radius =
                Mathf.SmoothDamp(Collider.radius, Radius,
                    ref RadiusVelocity, RadiusSmoothTime);
            
            MeshVelocityValueX =
                Mathf.SmoothDamp(MeshVelocityValueX, Radius * 2.0f, 
                    ref MeshVelocityX, RadiusSmoothTime);
            
            MeshVelocityValueY =
                Mathf.SmoothDamp(MeshVelocityValueY, Radius * 2.0f, 
                    ref MeshVelocityY, RadiusSmoothTime);
            
            MeshVelocityValueZ =
                Mathf.SmoothDamp(MeshVelocityValueZ, Radius * 2.0f, 
                    ref MeshVelocityZ, RadiusSmoothTime);

            SphereMesh.localScale = new Vector3(
                MeshVelocityValueX, MeshVelocityValueY, MeshVelocityValueZ);
            
            yield return new WaitForEndOfFrame();
        }
    }
    
    private IEnumerator SphereDisable()
    {
        float RadiusVelocity = 0.0f;
        
        float MeshVelocityValueX = SphereMesh.localScale.x;
        float MeshVelocityValueY = SphereMesh.localScale.y;
        float MeshVelocityValueZ = SphereMesh.localScale.z;
        
        float MeshVelocityX = 0.0f;
        float MeshVelocityY = 0.0f;
        float MeshVelocityZ = 0.0f;
        
        while (Collider.radius > 0.0f)
        {
            Collider.radius =
                Mathf.SmoothDamp(Collider.radius, 0.0f,
                    ref RadiusVelocity, RadiusSmoothTime);
            
            MeshVelocityValueX =
                Mathf.SmoothDamp(MeshVelocityValueX, 0.0f, 
                    ref MeshVelocityX, RadiusSmoothTime);
            
            MeshVelocityValueY =
                Mathf.SmoothDamp(MeshVelocityValueY, 0.0f, 
                    ref MeshVelocityY, RadiusSmoothTime);
            
            MeshVelocityValueZ =
                Mathf.SmoothDamp(MeshVelocityValueZ, 0.0f, 
                    ref MeshVelocityZ, RadiusSmoothTime);
            
            SphereMesh.localScale = new Vector3(
                MeshVelocityValueX, MeshVelocityValueY, MeshVelocityValueZ);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
