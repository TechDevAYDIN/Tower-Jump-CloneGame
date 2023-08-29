using UnityEngine;

public class RandomDeadPart : MonoBehaviour
{
    void Start()
    {
        MeshCollider[] childs = gameObject.GetComponentsInChildren<MeshCollider>();
        GameObject randomObject = childs[Random.Range(0, childs.Length)].gameObject;
        randomObject.AddComponent<DeadPart>();
        
            
    }
}
