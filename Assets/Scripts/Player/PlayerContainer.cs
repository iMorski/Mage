using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    public static PlayerContainer Instance;
    
    public Joystick Joystick;
    public Touch Touch;

    public float PowerInRate;
    public float PowerOutRate;

    private void Awake()
    {
        Instance = this;
    }
}
