using UnityEngine;

public class PlayerSphere : CharacterSphere
{
    private void Awake()
    {
        SphereCollider = GetComponent<SphereCollider>();
    }
    private void Start()
    {
        PlayerContainer.Instance.Touch.Tap += OnTap;
        PlayerContainer.Instance.Touch.Swipe += OnSwipe;
    }
    
    private void OnTap() { ChangeState(); }
    private void OnSwipe(Vector2 Direction) { ChangeState(); }
}
