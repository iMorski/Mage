public class PlayerSphere : CharacterSphere
{
    private void Start()
    {
        PlayerContainer.Instance.Touch.TouchBegin += OnTouchBegin;
        PlayerContainer.Instance.Touch.TouchFinish += OnTouchFinish;
    }

    private void OnTouchBegin() { ChangeState(); }
    private void OnTouchFinish() { ChangeState(); }
}
