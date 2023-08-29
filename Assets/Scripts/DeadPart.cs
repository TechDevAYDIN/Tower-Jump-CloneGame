using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DeadPart : MonoBehaviour
{
    private Material deadPartmat;
    private void Awake()
    {
        deadPartmat = Resources.Load<Material>("DeadPart");
    }
    private void OnEnable()
    {
        //GetComponentInChildren<Renderer>().material = deadPartmat;
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            r.material = deadPartmat;
        }
    }
    public void HitDeadPart()
    {
        FindObjectOfType<PlayerMovement>().Death();
       // GameManager.singleton.RestartLevel();
    }
}
