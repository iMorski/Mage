using UnityEngine;

public class ComputerContainer : MonoBehaviour
{
    public static ComputerContainer Instance;
    
    public float Distance;
    
    private GameObject[] BlockOnScene;

    private void Awake()
    {
        Instance = this;
        
        BlockOnScene = GameObject.FindGameObjectsWithTag("Block");
    }
    
    public GameObject GetBlock(Transform Position)
    {
        GameObject BlockInDistance = null;

        foreach (GameObject Block in BlockOnScene)
        {
            if (!BlockInDistance ||
                Vector3.Distance(Block.transform.position, Position.position) <
                Vector3.Distance(BlockInDistance.transform.position, Position.position))
            {
                BlockInDistance = Block;
            }
        }

        return BlockInDistance;
    }
}
