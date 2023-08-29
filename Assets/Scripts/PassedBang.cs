using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassedBang : MonoBehaviour
{
    public Animator anim;
    private Vector2 lastTapPos;

    void Update()
    {
        if (transform.parent.position.y >= GameManager.singleton.yPassed)
        {
            anim.SetTrigger("Passed");
        }
    }
    public void onAnimComplete()
    {
        Destroy(gameObject);
        Destroy(GetComponentInParent<BoxCollider>());
    }
}
