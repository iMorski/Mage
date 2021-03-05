using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiBar : MonoBehaviour
{
    public Transform Character;
    public Image Background;
    public Image Amount;
    
    public void OnCharacter()
    {
        transform.position = UiContainer.Instance.Camera.WorldToScreenPoint(
            Character.position) + new Vector3(0.0f, UiContainer.Instance.BarDistanceToCharacter, 0.0f);
    }

    public IEnumerator FadeIn()
    {
        while (Background.color.a < 1.0f || Amount.color.a < 1.0f)
        {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b,
                    Background.color.a + UiContainer.Instance.BarFadeRate * Time.fixedDeltaTime);
            Amount.color = new Color(Amount.color.r, Amount.color.g, Amount.color.b,
                Amount.color.a + UiContainer.Instance.BarFadeRate * Time.fixedDeltaTime);
            
            yield return new WaitForFixedUpdate();
        }
    }

    public IEnumerator FadeOut()
    {
        while (Background.color.a > 0.0f || Amount.color.a > 0.0f)
        {
            Background.color = new Color(Background.color.r, Background.color.g, Background.color.b,
                Background.color.a - UiContainer.Instance.BarFadeRate * Time.fixedDeltaTime);
            Amount.color = new Color(Amount.color.r, Amount.color.g, Amount.color.b,
                Amount.color.a - UiContainer.Instance.BarFadeRate * Time.fixedDeltaTime);
            
            yield return new WaitForFixedUpdate();
        }
    }
}
