using UnityEngine;

public class CharacterSpherePush : CharacterSphereCapture
{
    private Collider BlockOnSelect;
    
    private void FixedUpdate()
    {
        Collider BlockInDistance = null;

        foreach (Collider Block in BlockInSphereCollider)
        {
            if (!BlockInDistance ||
                Vector3.Distance(Block.transform.position, transform.position) <
                Vector3.Distance(BlockInDistance.transform.position, transform.position))
            {
                BlockInDistance = Block;
            }
        }

        if (!BlockInDistance) BlockOnSelect = null;
        else if (BlockInDistance != BlockOnSelect) BlockOnSelect = BlockInDistance;
    }
    
    public void Push(Vector2 Direction)
    {
        if (!BlockOnSelect) return;
        
        Rigidbody Rigidbody = BlockOnSelect.GetComponent<Rigidbody>();
        Coroutine Coroutine = BlockInSphereCoroutine[
            BlockInSphereCollider.IndexOf(BlockOnSelect)];
        
        if (Coroutine != null) StopCoroutine(Coroutine);
        
        Rigidbody.AddForce(new Vector3(Direction.x, 0.0f,
            Direction.y) * CharacterContainer.Instance.SpherePushForce);
    }
}
