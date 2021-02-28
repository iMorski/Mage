public class PlayerMovement : CharacterMovement
{
    private Joystick Joystick;

    private void Start()
    {
        Joystick = PlayerContainer.Instance.Joystick;
        
        PlayerContainer.Instance.Touch.TouchBegin += OnTouchBegin;
        PlayerContainer.Instance.Touch.TouchFinish += OnTouchFinish;
    }

    private void OnTouchBegin() { ChangeState(); }
    private void OnTouchFinish() { ChangeState(); }
    private void FixedUpdate() { ChangePosition(Joystick.Direction); }
}
