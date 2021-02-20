using UnityEngine;

public class CameraOnCharacter : MonoBehaviour
{
    [SerializeField] private float SmoothTime;
    [SerializeField] private Transform Character;

    private float PositionX;
    private float PositionY;
    private float PositionZ;

    private void Awake()
    {
        PositionX = transform.position.x;
        PositionY = transform.position.y;
        PositionZ = transform.position.z;
    }

    private Vector3 Velocity;

    private void Update()
    {
        Vector3 OnCharacterPosition = new Vector3(PositionX + Character.position.x,
            PositionY, PositionZ + Character.position.z);
        Vector3 OnCharacterPositionSmooth = Vector3.SmoothDamp(transform.position,
            OnCharacterPosition, ref Velocity, SmoothTime);
        
        transform.position = OnCharacterPositionSmooth;
    }
}
