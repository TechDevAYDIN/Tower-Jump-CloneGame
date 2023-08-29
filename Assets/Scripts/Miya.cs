using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miya : MonoBehaviour
{
    private Material goalPartMat;
    private void Awake()
    {
        goalPartMat = Resources.Load<Material>("GoalPart");
    }
    private void OnEnable()
    {
        Renderer[] renderers = this.gameObject.GetComponentInParent<LineRenderer>().gameObject.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            r.material = goalPartMat;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameManager.singleton.GoalReached();
        Destroy(GetComponent<BoxCollider>());
    }
}
