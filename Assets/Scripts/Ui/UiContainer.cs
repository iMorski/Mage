using UnityEngine;

public class UiContainer : MonoBehaviour
{
    public static UiContainer Instance;

    public Camera Camera;
    public float BarDistance;
    public Color BarPowerColorMain;
    public Color BarPowerColorOnCharge;

    private void Awake()
    {
        Instance = this;
    }
}
