using System.Collections;
using UnityEngine;

public class CharacterSphere : MonoBehaviour
{
    [SerializeField] private float Radius;
    [SerializeField] private float RadiusSmoothTime;
    
    private Touch Touch;
    private SphereCollider Collider;

    private float VelocityOnEnable;
    private float VelocityOnDisable;

    private void Awake()
    {
        Touch = GetComponentInParent<CharacterMovement>().Touch;
        Collider = GetComponentInChildren<SphereCollider>();
    }

    private void Start()
    {
        Touch.TouchBegin += OnTouchBegin;
        Touch.TouchFinish += OnTouchFinish;
    }

    private void OnTouchBegin()
    {
        StopAllCoroutines();
        StartCoroutine(SphereEnable());
    }
    
    private void OnTouchFinish()
    {
        StopAllCoroutines();
        StartCoroutine(SphereDisable());
    }

    private IEnumerator SphereEnable()
    {
        while (Collider.radius < Radius)
        {
            Collider.radius =
                Mathf.SmoothDamp(Collider.radius, Radius, ref VelocityOnEnable, RadiusSmoothTime);
            
            yield return new WaitForEndOfFrame();
        }
    }
    
    private IEnumerator SphereDisable()
    {
        while (Collider.radius > 0.0f)
        {
            Collider.radius =
                Mathf.SmoothDamp(Collider.radius, 0.0f, ref VelocityOnDisable, RadiusSmoothTime);
            
            yield return new WaitForEndOfFrame();
        }
    }
}
