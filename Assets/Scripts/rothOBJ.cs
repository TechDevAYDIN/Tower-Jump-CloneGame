using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rothOBJ : MonoBehaviour
{
    private Vector2 lastTapPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && GameManager.singleton.isAlive)
        {

            Vector2 curTapPos = Input.mousePosition;

            if (lastTapPos == Vector2.zero)
                lastTapPos = curTapPos;

            float delta = lastTapPos.x - curTapPos.x;
            lastTapPos = curTapPos;

            transform.Rotate(Vector3.up * (delta /2));
            //transform.eulerAngles = Vector3.SlerpUnclamped(transform.eulerAngles, Vector3.up * (delta / 2), Time.deltaTime * 0.05f);
            //transform.Rotate(Vector3.up * Mathf.Lerp(transform.localEulerAngles.y, delta, Time.deltaTime*10f));
        }

        if (Input.GetMouseButtonUp(0))
        {
            lastTapPos = Vector2.zero;
        }
    }
}
