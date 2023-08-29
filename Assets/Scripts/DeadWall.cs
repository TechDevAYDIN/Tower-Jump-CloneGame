using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadWall : MonoBehaviour
{
    private Material deadPartmat;
    private void Awake()
    {
        deadPartmat = Resources.Load<Material>("DeadPart");
    }
    private void OnEnable()
    {
        //GetComponentInChildren<Renderer>().material.color = Color.red;
        GetComponentInChildren<Renderer>().material = deadPartmat;
    }
    public void HitDeadWall()
    {
        FindObjectOfType<PlayerMovement>().Death();
        // GameManager.singleton.RestartLevel();
    }
}
