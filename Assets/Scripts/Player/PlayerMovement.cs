public class PlayerMovement : CharacterMovement
{
    private Joystick Joystick;

    private void Start()
    {
        Joystick = PlayerContainer.Instance.Joystick;

        PlayerSphere PlayerSphere = GetComponentInChildren<PlayerSphere>();
        
        PlayerSphere.CastBegin += OnCastBegin;
        PlayerSphere.CastFinish += OnCastFinish;
    }

    private void OnCastBegin() { ChangeState(); }
    private void OnCastFinish() { ChangeState(); }
    
    private void FixedUpdate() { ChangePosition(Joystick.Direction); }
}
