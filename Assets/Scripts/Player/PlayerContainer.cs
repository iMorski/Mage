using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    public static PlayerContainer Instance;
    
    public Joystick Joystick;
    public Touch Touch;
    public Touch TouchSphere;
    public Touch TouchDash;

    public float PowerUpRate;

    private void Awake()
    {
        Instance = this;
    }
}
