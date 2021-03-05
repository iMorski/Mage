using UnityEngine;

public class UiContainer : MonoBehaviour
{
    public static UiContainer Instance;

    public Camera Camera;
    
    public float BarDistanceToCharacter;
    public float BarDistanceToBlock;
    
    public float BarFadeRate;

    private void Awake()
    {
        Instance = this;
    }
}
