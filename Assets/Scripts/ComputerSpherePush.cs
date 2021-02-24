using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ComputerSpherePush : MonoBehaviour
{
    [SerializeField] private float WaitMin;
    [SerializeField] private float WaitMax;
    
    private GameObject Player;
    
    private CharacterSphereCapture CharacterSphereCapture;

    private void Awake()
    {
        CharacterSphereCapture = GetComponent<CharacterSphereCapture>();
        
        Player = GameObject.FindWithTag("Player");
    }

    private Collider BlockOnPush;

    private void FixedUpdate()
    {
        Collider Block = CharacterSphereCapture.BlockCollider;

        if (Block)
        {
            if (Block != BlockOnPush)
            {
                StartCoroutine(Push(Block));
            }
        }
        else
        {
            BlockOnPush = null;
        }
    }

    private IEnumerator Push(Collider Block)
    {
        BlockOnPush = Block;
        
        Debug.Log(Block);

        yield return new WaitForSeconds(Random.Range(WaitMin, WaitMax));

        Vector3 PlayerPosition = Player.transform.position;
        Vector3 Position = transform.position;
        
        BlockOnPush.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(
            PlayerPosition - Position) * CharacterContainer.Instance.SpherePushForce);
    }
}
