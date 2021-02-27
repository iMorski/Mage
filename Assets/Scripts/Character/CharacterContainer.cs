using UnityEngine;

public class CharacterContainer : MonoBehaviour
{
    public static CharacterContainer Instance;
    
    [Tooltip("Move Speed")] public float MoveSpeed;
    [Tooltip("Move Smooth Time")] public float MoveSmoothTime;
    
    [Tooltip("Sphere Radius")] public float SphereRadius;
    [Tooltip("Sphere Smooth Time")] public float SphereSmoothTime;

    [Tooltip("Sphere Capture Rise Distance")] public float SphereCaptureRiseDistance;
    [Tooltip("Sphere Capture Rise Force")] public float SphereCaptureRiseForce;
    [Tooltip("Sphere Capture Rise Time")] public float SphereCaptureRiseTime;
    [Tooltip("Sphere Capture Time")] public float SphereCaptureTime;

    [Tooltip("Sphere Push Force")] public float SpherePushForce;

    private void Awake()
    {
        Instance = this;
    }
}
