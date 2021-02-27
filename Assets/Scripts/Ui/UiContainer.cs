using UnityEngine;

public class UiContainer : MonoBehaviour
{
    public static UiContainer Instance;

    public float BarDistanceToCharacter;
    public float BarDistanceToBlock;
    
    public float BarFadeRate;
    
    public float BarStaminaIncreaseRate;
    public float BarStaminaDecreaseRate;

    private void Awake()
    {
        Instance = this;
    }
}
