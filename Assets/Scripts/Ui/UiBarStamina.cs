using UnityEngine;

public class UiBarStamina : UiBarController
{
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private PlayerSphere PlayerSphere;

    private void Start()
    {
        //PlayerSphere.ChangeCast += OnChangeCast;
    }

    private void OnChangeCast(bool OnCast)
    {
        StopAllCoroutines();
        
        if (OnCast)
        {
            StartCoroutine(ValueDecrease());
        }
        else
        {
            StartCoroutine(ValueIncrease());
        }
    }
}
