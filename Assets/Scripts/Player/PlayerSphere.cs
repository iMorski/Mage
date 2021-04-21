using UnityEngine;

public class PlayerSphere : CharacterSphere
{
    public delegate void OnCastBegin();
    public event OnCastBegin CastBegin;
    
    public delegate void OnCastFinish();
    public event OnCastFinish CastFinish;

    private PlayerPower PlayerPower;
    private void Awake() { PlayerPower = GetComponentInParent<PlayerPower>(); }

    private void Start()
    {
        PlayerContainer.Instance.TouchSphere.TapBegin += OnTapBegin;
        PlayerContainer.Instance.TouchSphere.TapFinish += OnTapFinish;
    }

    private bool Cast;
    
    private void OnTapBegin()
    {
        if (!(PlayerPower.PowerCharge01 < 1.0f))
        {
            CastBegin?.Invoke();
            
            ChangeState();

            Cast = !Cast;
        }
    }

    private void OnTapFinish()
    {
        if (OnCast)
        {
            CastFinish?.Invoke();
            
            ChangeState();
            
            Cast = !Cast;
        }
    }
}
