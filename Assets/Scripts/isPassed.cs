using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isPassed : MonoBehaviour
{
    private Vector2 lastTapPos;
    private Vector3 fixRot;
    private Vector3 fixRotThis;

    private GameObject centerMain;

    private void Awake()
    {
        centerMain = FindObjectOfType<CenterController>().gameObject;
    }
    private void OnTriggerEnter(Collider other)
    {
        GameManager.singleton.yPassed = transform.position.y;
        FindObjectOfType<PlayerMovement>().perfectPass++;
        AudioManage.PlaySound(2);
        AudioManage.SetPitch(.2f);
        GameManager.singleton.AddScore((GameManager.singleton.currentStage + 1) * FindObjectOfType<PlayerMovement>().perfectPass);
    }
    private void Update()
    {
        if(transform.position.y == GameManager.singleton.yPassed)
        {
            fixRotThis = transform.rotation.eulerAngles;
            GameManager.singleton.yPassed -= 0.2f;
        }
        if(transform.position.y >= GameManager.singleton.yPassed - 2)
        {
            transform.rotation = Quaternion.Euler(0,fixRotThis.y , 0);
        }
    }
}
