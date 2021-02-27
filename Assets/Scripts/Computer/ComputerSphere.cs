using UnityEngine;

public class ComputerSphere : CharacterSphere
{
    private void Awake()
    {
        SphereCollider = GetComponent<SphereCollider>();
    }

    private void FixedUpdate()
    {
        GameObject Block = ComputerContainer.Instance.GetBlock(transform);
        
        Vector3 BlockPosition = Block.transform.position;
        Vector3 Position = transform.position;

        float Distance = Vector3.Distance(BlockPosition, Position);

        if (Distance > ComputerContainer.Instance.Distance && OnCast ||
            Distance < ComputerContainer.Instance.Distance && !OnCast)
        {
            ChangeState();
        }
    }
}
