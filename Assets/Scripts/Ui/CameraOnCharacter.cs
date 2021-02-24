using UnityEngine;

public class CameraOnCharacter : MonoBehaviour
{
    [SerializeField] private float SmoothTime;
    [SerializeField] private Transform Character;

    private Vector3 Position;

    private void Awake()
    {
        Position = transform.position;
    }

    private Vector3 Velocity;

    private void Update()
    {
        Vector3 OnCharacterPosition = new Vector3(Position.x + Character.position.x,
            Position.y, Position.z + Character.position.z);
        Vector3 OnCharacterPositionSmooth = Vector3.SmoothDamp(transform.position,
            OnCharacterPosition, ref Velocity, SmoothTime);
        
        transform.position = OnCharacterPositionSmooth;
    }
}
