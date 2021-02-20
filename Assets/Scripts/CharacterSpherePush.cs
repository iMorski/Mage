using UnityEngine;

public class CharacterSpherePush : MonoBehaviour
{
    [SerializeField] private float ForceMultiplier;

    private CharacterSphereCapture SphereCapture;

    private void Awake()
    {
        SphereCapture =
            GetComponent<CharacterSphereCapture>();
    }

    private void Start()
    {
        GetComponentInParent<CharacterContainer>().Touch.Swipe +=
            OnSwipe;
    }

    private void OnSwipe(Vector2 Direction)
    {
        if (!SphereCapture.PushRigidbody) return;
        
        SphereCapture.StopAllCoroutines();
        SphereCapture.PushRigidbody.AddForce(new Vector3
            (Direction.x, 0.0f, Direction.y) * ForceMultiplier);
    }
}
