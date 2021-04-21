using UnityEngine;
using UnityEngine.UI;

public class UiBarPower : MonoBehaviour
{
    [SerializeField] private Transform Character;
    [SerializeField] private Image Bar01Amount;
    [SerializeField] private Image Bar02Amount;
    [SerializeField] private Image Bar03Amount;
    
    private PlayerPower PlayerPower;

    private void Awake()
    {
        PlayerPower = Character.GetComponent<PlayerPower>();
    }

    private bool Bar01ColorChange;
    private bool Bar02ColorChange;
    private bool Bar03ColorChange;

    private void FixedUpdate()
    {
        float BarDistanceToCharacterInView = UiContainer.Instance.Camera.ViewportToScreenPoint(
            new Vector3(0.0f, UiContainer.Instance.BarDistance, 0.0f)).y;
        
        transform.position = UiContainer.Instance.Camera.WorldToScreenPoint(
            Character.position) + new Vector3(0.0f, BarDistanceToCharacterInView, 0.0f);
        
        Bar01Amount.fillAmount = PlayerPower.PowerCharge01;
        Bar02Amount.fillAmount = PlayerPower.PowerCharge02;
        Bar03Amount.fillAmount = PlayerPower.PowerCharge03;

        if (PlayerPower.PowerCharge03 < 1.0f && !Bar03ColorChange)
        {
            Bar03Amount.color = UiContainer.Instance.BarPowerColorOnCharge;
            
            Bar03ColorChange = !Bar03ColorChange;
        }
        else if (!(PlayerPower.PowerCharge03 < 1.0f) && Bar03ColorChange)
        {
            Bar03Amount.color = UiContainer.Instance.BarPowerColorMain;
            
            Bar03ColorChange = !Bar03ColorChange;
        }
        
        if (PlayerPower.PowerCharge02 < 1.0f && !Bar02ColorChange)
        {
            Bar02Amount.color = UiContainer.Instance.BarPowerColorOnCharge;
            
            Bar02ColorChange = !Bar02ColorChange;
        }
        else if (!(PlayerPower.PowerCharge02 < 1.0f) && Bar02ColorChange)
        {
            Bar02Amount.color = UiContainer.Instance.BarPowerColorMain;
            
            Bar02ColorChange = !Bar02ColorChange;
        }
        
        if (PlayerPower.PowerCharge01 < 1.0f && !Bar01ColorChange)
        {
            Bar01Amount.color = UiContainer.Instance.BarPowerColorOnCharge;
            
            Bar01ColorChange = !Bar01ColorChange;
        }
        else if (!(PlayerPower.PowerCharge01 < 1.0f) && Bar01ColorChange)
        {
            Bar01Amount.color = UiContainer.Instance.BarPowerColorMain;
            
            Bar01ColorChange = !Bar01ColorChange;
        }
    }
}
