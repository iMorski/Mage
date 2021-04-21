using System;
using System.Collections;
using UnityEngine;

public class PlayerPower : MonoBehaviour
{
    [NonSerialized] public float PowerCharge01 = 1.0f;
    [NonSerialized] public float PowerCharge02 = 1.0f;
    [NonSerialized] public float PowerCharge03 = 1.0f;
    
    private void Start()
    {
        PlayerSphere PlayerSphere = GetComponentInChildren<PlayerSphere>();
        
        PlayerSphere.CastBegin += OnCastBegin;
        PlayerSphere.CastFinish += OnCastFinish;
    }

    private bool OnCast;

    private void OnCastBegin()
    {
        OnCast = !OnCast;
        
        StopAllCoroutines();
        
        if (!(PowerCharge03 < 1.0f))
        {
            PowerCharge03 = 0.0f;
        }
        else if (!(PowerCharge02 < 1.0f))
        {
            PowerCharge02 = PowerCharge03;
            PowerCharge03 = 0.0f;
            
            //StartCoroutine(PowerUpCharge02());
        }
        else if (!(PowerCharge01 < 1.0f))
        {
            PowerCharge01 = PowerCharge02;
            PowerCharge02 = 0.0f;
            
            //StartCoroutine(PowerUpCharge01());
        }
    }

    private void OnCastFinish()
    {
        OnCast = !OnCast;
        
        StopAllCoroutines();
        
        if (PowerCharge01 < 1.0f) StartCoroutine(PowerUpCharge01());
        else if (PowerCharge02 < 1.0f) StartCoroutine(PowerUpCharge02());
        else if (PowerCharge03 < 1.0f) StartCoroutine(PowerUpCharge03());
    }

    private IEnumerator PowerUpCharge01()
    {
        while (PowerCharge01 < 1.0f)
        {
            PowerCharge01 = PowerCharge01 + PlayerContainer.Instance.PowerUpRate * Time.fixedDeltaTime;
            
            yield return new WaitForFixedUpdate();
        }
        
        if (!OnCast) StartCoroutine(PowerUpCharge02());
    }
    
    private IEnumerator PowerUpCharge02()
    {
        while (PowerCharge02 < 1.0f)
        {
            PowerCharge02 = PowerCharge02 + PlayerContainer.Instance.PowerUpRate * Time.fixedDeltaTime;
            
            yield return new WaitForFixedUpdate();
        }
        
        if (!OnCast) StartCoroutine(PowerUpCharge03());
    }
    
    private IEnumerator PowerUpCharge03()
    {
        while (PowerCharge03 < 1.0f)
        {
            PowerCharge03 = PowerCharge03 + PlayerContainer.Instance.PowerUpRate * Time.fixedDeltaTime;
            
            yield return new WaitForFixedUpdate();
        }
    }
}
