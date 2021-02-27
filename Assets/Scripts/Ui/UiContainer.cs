using UnityEngine;

public class UiContainer : MonoBehaviour
{
    public static UiContainer Instance;

    [Tooltip("Bar Distance To Character")] public float BarDistanceToCharacter;
    [Tooltip("Bar Distance To Block")] public float BarDistanceToBlock;
    
    [Tooltip("Bar Fade Rate")] public float BarFadeRate;
    
    [Tooltip("Bar Stamina Increase Rate")] public float BarStaminaIncreaseRate;
    [Tooltip("Bar Stamina Decrease Rate")] public float BarStaminaDecreaseRate;

    private void Awake()
    {
        Instance = this;
    }
}
