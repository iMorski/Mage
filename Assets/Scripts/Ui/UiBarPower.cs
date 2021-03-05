using UnityEngine;

public class UiBarPower : UiBar
{
    private PlayerPower PlayerPower;

    private void Awake() { PlayerPower = Character.GetComponent<PlayerPower>(); }

    private Coroutine Coroutine;
    
    private bool OnBar;

    private void FixedUpdate()
    {
        OnCharacter();
        
        if (!OnBar && PlayerPower.Power < 1.0f)
        {
            if (Coroutine != null) StopCoroutine(Coroutine);
            
            Coroutine = StartCoroutine(FadeIn());

            OnBar = !OnBar;
        }
        else if (OnBar && !(PlayerPower.Power < 1.0f))
        {
            if (Coroutine != null) StopCoroutine(Coroutine);
            
            Coroutine = StartCoroutine(FadeOut());

            OnBar = !OnBar;
        }
        
        Amount.fillAmount = PlayerPower.Power;
    }
}
