using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiBarController : MonoBehaviour
{
    public Camera Camera;
    public Transform Character;
    
    public Image Background;
    public Image Amount;

    private void FixedUpdate()
    {
        transform.position = Camera.WorldToScreenPoint(Character.position) + new Vector3(
            0.0f, UiContainer.Instance.BarDistanceToCharacter, 0.0f);
    }

    public IEnumerator ValueIncrease()
    {
        while (Amount.fillAmount < 1.0f)
        {
            float IncreaseRate = UiContainer.Instance.BarStaminaIncreaseRate;
            float IncreaseRateSmooth = IncreaseRate * Time.fixedDeltaTime;
            
            Amount.fillAmount = Amount.fillAmount + IncreaseRateSmooth;
            
            yield return new WaitForFixedUpdate();
        }
        
        StartCoroutine(OpacityDecrease());
    }
    
    public IEnumerator ValueDecrease()
    {
        StartCoroutine(OpacityIncrease());
        
        while (Amount.fillAmount > 0.0f)
        {
            float DecreaseRate = UiContainer.Instance.BarStaminaDecreaseRate;
            float DecreaseRateSmooth = DecreaseRate * Time.fixedDeltaTime;
            
            Amount.fillAmount = Amount.fillAmount - DecreaseRateSmooth;
            
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator OpacityIncrease()
    {
        while (Amount.color.a < 1.0f)
        {
            float FadeInTime = UiContainer.Instance.BarFadeRate;
            float FadeInTimeSmooth = FadeInTime * Time.fixedDeltaTime;

            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b,
                Background.color.a + FadeInTimeSmooth);
            Amount.color = new Color(Amount.color.r, Amount.color.g, Amount.color.b,
                Amount.color.a + FadeInTimeSmooth);
            
            yield return new WaitForFixedUpdate();
        }
    }
    
    private IEnumerator OpacityDecrease()
    {
        while (Amount.color.a > 0.0f)
        {
            float FadeOutTime = UiContainer.Instance.BarFadeRate;
            float FadeOutTimeSmooth = FadeOutTime * Time.fixedDeltaTime;

            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b,
                Background.color.a - FadeOutTimeSmooth);
            Amount.color = new Color(Amount.color.r, Amount.color.g, Amount.color.b,
                Amount.color.a - FadeOutTimeSmooth);
            
            yield return new WaitForFixedUpdate();
        }
    }
}
