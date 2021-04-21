using UnityEngine;

public class UiButton : Touch
{
    private Animator Animator;
    
    private void Awake()
    {
        Animator = GetComponentInParent<Animator>();
        
        TapBegin += OnTouchTapBegin;
        TapFinish += OnTouchTapFinish;
    }

    private void OnTouchTapBegin()
    {
        Animator.Play("Ui-Push-In");
    }
    
    private void OnTouchTapFinish()
    {
        Animator.Play("Ui-Push-Out");
    }
}
