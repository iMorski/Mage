public class PlayerSphere : CharacterSphere
{
    public delegate void OnCastBegin();
    public event OnCastBegin CastBegin;
    
    public delegate void OnCastFinish();
    public event OnCastFinish CastFinish;

    private void Start()
    {
        PlayerContainer.Instance.Touch.TapBegin += OnTapBegin;
        PlayerContainer.Instance.Touch.TapFinish += OnTapFinish;
    }

    private void OnTapBegin() { CastBegin?.Invoke(); ChangeState(); }
    private void OnTapFinish() { CastFinish?.Invoke(); ChangeState(); }
}
