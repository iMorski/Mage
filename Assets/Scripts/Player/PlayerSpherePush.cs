using UnityEngine;

public class PlayerSpherePush : CharacterSpherePush
{
    private void Start()
    {
        PlayerContainer.Instance.Touch.SwipeRelease += OnSwipeRelease;
    }

    private void OnSwipeRelease(Vector2 Direction) { Push(Direction); }
}
