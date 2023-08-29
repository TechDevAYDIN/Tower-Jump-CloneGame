using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerMovement target;
    private float offset;
    private void Awake()
    {
        offset = transform.position.y - target.transform.position.y + 1.8f ;
    }
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 curpos = transform.position;
        curpos.y = target.transform.position.y + offset;
        if(transform.position.y > curpos.y)
        {
            transform.position = curpos;
        }
    }
}
