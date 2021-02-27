using UnityEngine;

public class UiCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform Character;
    [SerializeField] private float SmoothTime;

    private Vector3 Position;

    private void Awake()
    {
        Position = transform.position;
    }

    private Vector3 Velocity;

    private void FixedUpdate()
    {
        Vector3 CharacterPosition = new Vector3(Position.x + Character.position.x,
            Position.y, Position.z + Character.position.z);
        Vector3 CharacterPositionSmooth = Vector3.SmoothDamp(transform.position,
            CharacterPosition, ref Velocity, SmoothTime);
        
        transform.position = CharacterPositionSmooth;
    }
}
