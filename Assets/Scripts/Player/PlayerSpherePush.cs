using UnityEngine;

public class PlayerSpherePush : CharacterSpherePush
{
    private void Start()
    {
        PlayerContainer.Instance.Touch.SwipeByRelease += OnSwipeByRelease;
    }

    private void OnSwipeByRelease(Vector2 Direction) { Push(Direction); }
}
