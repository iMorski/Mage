using UnityEngine;

public class PlayerSpherePush : MonoBehaviour
{
    private CharacterSphereCapture CharacterSphereCapture;
    
    private void Awake()
    {
        CharacterSphereCapture = GetComponent<CharacterSphereCapture>();
    }

    private void Start()
    {
        PlayerContainer.Instance.Touch.Swipe += OnSwipe;
    }

    private void OnSwipe(Vector2 Direction)
    {
        if (!CharacterSphereCapture.BlockCollider) return;
        
        CharacterSphereCapture.BlockCollider.GetComponent<Rigidbody>().AddForce(new Vector3(
            Direction.x, 0.0f, Direction.y) * CharacterContainer.Instance.SpherePushForce);
    }
}
