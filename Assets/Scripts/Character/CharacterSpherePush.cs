using UnityEngine;

public class CharacterSpherePush : CharacterSphereCapture
{
    public void Push(Vector2 Direction)
    {
        if (!BlockOnSelect) return;
        
        Rigidbody Rigidbody = BlockOnSelect.GetComponent<Rigidbody>();
        Coroutine Coroutine = BlockInSphereCoroutine[
            BlockInSphereCollider.IndexOf(BlockOnSelect)];
        
        if (Coroutine != null) StopCoroutine(Coroutine);
        
        Rigidbody.AddForce(new Vector3(
            Direction.x, 0.0f, Direction.y) * CharacterContainer.Instance.SpherePushForce);
    }
}
