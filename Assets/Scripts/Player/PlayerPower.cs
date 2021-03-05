using System;
using System.Collections;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [NonSerialized] public float Power = 1.0f;
    
    private void Start()
    {
        PlayerSphere PlayerSphere = GetComponentInChildren<PlayerSphere>();
        
        PlayerSphere.CastBegin += OnCastBegin;
        PlayerSphere.CastFinish += OnCastFinish;
    }
    
    private Coroutine Coroutine;

    private void OnCastBegin() { Coroutine = StartCoroutine(PowerOut()); }
    private void OnCastFinish() { Coroutine = StartCoroutine(PowerIn()); }
    
    private IEnumerator PowerIn()
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        
        while (Power < 1.0f)
        {
            Power = Power + PlayerContainer.Instance.PowerInRate * Time.fixedDeltaTime;
            
            yield return new WaitForFixedUpdate();
        }
    }
    
    private IEnumerator PowerOut()
    {
        if (Coroutine != null) StopCoroutine(Coroutine);
        
        while (Power > 0.0f)
        {
            Power = Power - PlayerContainer.Instance.PowerOutRate * Time.fixedDeltaTime;
            
            yield return new WaitForFixedUpdate();
        }
    }
}
