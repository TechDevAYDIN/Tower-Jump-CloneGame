using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float ImpulseForce;
    private bool ignoreNextCollision;
    
    private Vector3 startPos;

    public int perfectPass = 0;
    public bool isSSActive;

    public GameObject splashImg;
    public GameObject splashParticle;
    public GameObject splashParticle2;

    public GameObject glowPrefab;

    public ParticleSystem smoke2;

    private Color startBallColor;
    private Material goalPartMat;
    // Start is called before the first frame update
    [System.Obsolete]
    void Awake()
    {
        goalPartMat = Resources.Load<Material>("GoalPart");
        GameManager.singleton.isAlive = true;
        startPos = transform.position;
        startBallColor = rb.GetComponentInParent<Renderer>().material.color;
        splashImg.GetComponentInChildren<SpriteRenderer>().color = startBallColor;
        splashParticle.GetComponentInChildren<ParticleSystem>().startColor = startBallColor;
        splashParticle2.GetComponentInChildren<ParticleSystem>().startColor = startBallColor;
        GetComponentInChildren<TrailRenderer>().startColor = startBallColor;
        GetComponentInChildren<ParticleSystem>().startColor = new Color(startBallColor.r, startBallColor.g, startBallColor.b, 0.5f);
        smoke2.startColor = new Color(startBallColor.r, startBallColor.g, startBallColor.b);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (ignoreNextCollision)
            return;

        if (isSSActive)
        {
            if (!collision.gameObject.GetComponent<LineRenderer>())
            {      
                Renderer[] renderers = collision.gameObject.GetComponentInParent<BoxCollider>().gameObject.GetComponentsInChildren<Renderer>();
                foreach (var r in renderers)
                {
                    r.material = goalPartMat;
                }
                DeadWall[] deadWalls = collision.gameObject.GetComponentInParent<BoxCollider>().gameObject.GetComponentsInChildren<DeadWall>();
                foreach (var d in deadWalls)
                {
                    Destroy(d.GetComponent<DeadWall>());
                }
                GameManager.singleton.yPassed = collision.transform.position.y;
                Instantiate(splashParticle2, transform.position + new Vector3(0f, 0.51f, -1.7f), Quaternion.identity, collision.gameObject.transform.parent.parent.parent);
                perfectPass = 0;
                isSSActive = false;
                ignoreNextCollision = true;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * ImpulseForce, ForceMode.Impulse);
                AudioManage.ResetPitch(1.25f);
                AudioManage.PlaySound(1);
                Invoke("AllowCollision", .05f);          
            }            
        }
        else
        {
            var normal = collision.contacts[0].normal;
            DeadPart deadPart = collision.transform.GetComponentInParent<DeadPart>();
            DeadWall deadWall = collision.transform.GetComponentInParent<DeadWall>();
            if (deadPart && normal.y > 0.5f)
            {
                ignoreNextCollision = true;
                deadPart.HitDeadPart();
            }
            if (deadWall)
            {
                ignoreNextCollision = true;
                deadWall.HitDeadWall();
            }
            if (normal.y > 0.5f && !deadPart && GameManager.singleton.isAlive)
            {
                if (gameObject.GetComponent<JellyMesh>().Intensity == 0)
                    AfterLoad();
                Instantiate(splashImg, collision.gameObject.transform.position + new Vector3(0f, 0.51f, -1.7f), Quaternion.Euler(new Vector3(90, Random.Range(0, 360), 0)), collision.gameObject.transform);
                Instantiate(splashParticle, collision.gameObject.transform.position + new Vector3(0f, 0.51f, -1.7f), Quaternion.identity, collision.gameObject.transform);
                //Secure Time
                ignoreNextCollision = true;
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * ImpulseForce, ForceMode.Impulse);
                AudioManage.ResetPitch(1.25f);
                AudioManage.PlaySound(1);
                Invoke("AllowCollision", .1f);
                perfectPass = 0;
                isSSActive = false;

            }
        }

        

        
    }
    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        
        if (rb.velocity.y < -8)
            rb.AddForce(Vector3.up * 10);
        /*
        if(splashImg.GetComponentInChildren<SpriteRenderer>().color != rb.GetComponentInParent<Renderer>().material.color || splashParticle2.GetComponentInChildren<ParticleSystem>().startColor != rb.GetComponentInParent<Renderer>().material.color)
        {
            splashImg.GetComponentInChildren<SpriteRenderer>().color = rb.GetComponentInParent<Renderer>().material.color;
            splashParticle.GetComponentInChildren<ParticleSystem>().startColor = rb.GetComponentInParent<Renderer>().material.color;
            splashParticle2.GetComponentInChildren<ParticleSystem>().startColor = rb.GetComponentInParent<Renderer>().material.color;
            GetComponentInChildren<TrailRenderer>().startColor = rb.GetComponentInParent<Renderer>().material.color;
        }*/
        if (perfectPass == 0)
        {
            AudioManage.ResetPitch(1.25f);
            GetComponentInChildren<ParticleSystem>().Stop();
            GetComponentInChildren<ParticleSystem>().Clear();
            smoke2.Stop();
            smoke2.Clear();
            ResetColor();
            Animator[] anims = gameObject.GetComponentsInChildren<Animator>();
            foreach (var a in anims)
            {
                Destroy(a.gameObject);
            }
        }
        if(perfectPass == 2)
        {
            smoke2.startSize = 0.25f;
            GetComponentInChildren<ParticleSystem>().Play();
            smoke2.Play();
        }
        if (perfectPass >= 3)
        {
            if (!isSSActive)
            {
                smoke2.startSize = 0.5f;
                GetComponentInChildren<ParticleSystem>().Stop();
                GetComponentInChildren<ParticleSystem>().Clear();
                smoke2.Stop();
                smoke2.Clear();
                isSSActive = true;
                rb.GetComponentInParent<Renderer>().material.color = Color.red;
                GetComponentInChildren<TrailRenderer>().startColor = Color.red;
                GetComponentInChildren<ParticleSystem>().startColor = new Color(1f,0,0,.5f);
                smoke2.startColor = new Color(1f, 0, 0, .5f);
                GetComponentInChildren<ParticleSystem>().Play();
                smoke2.Play();
                MakeGlow();
                Invoke("MakeGlow", .2f);
                Invoke("MakeGlow", .6f);
            }
        }
        if (GameManager.singleton.isAlive && rb.velocity.y == 0)
        {
            Invoke("Jump", .1f);
        }
    }
    private void AllowCollision()
    {
        perfectPass = 0;
        ignoreNextCollision = false;
    }
    public void ResetBall()
    {
        gameObject.GetComponent<JellyMesh>().Intensity = 0;
        gameObject.GetComponentInChildren<TrailRenderer>().time = 0;
        transform.position = startPos;
        Camera.main.transform.position = new Vector3(0,4,-9.5f);
        GameManager.singleton.isAlive = true;
        rb.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        AllowCollision();
    }
    public void AfterLoad()
    {
        GameManager.singleton.isAlive = true;
        gameObject.GetComponent<JellyMesh>().Intensity = 1;
        gameObject.GetComponentInChildren<TrailRenderer>().time = 0.4f;
    }
    public void Death()
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        smoke2.Stop();
        GameManager.singleton.isAlive = false;
        perfectPass = 0;
        GameManager.singleton.DeadCanvas();
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.gameObject.transform.localScale = new Vector3(0.5f, 0.35f, 0.5f);
        rb.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
    }
    [System.Obsolete]
    public void ResetColor()
    {
        rb.GetComponentInParent<Renderer>().material.color = startBallColor;
        splashImg.GetComponentInChildren<SpriteRenderer>().color = startBallColor;
        splashParticle.GetComponentInChildren<ParticleSystem>().startColor = startBallColor;
        splashParticle2.GetComponentInChildren<ParticleSystem>().startColor = startBallColor;
        GetComponentInChildren<TrailRenderer>().startColor = startBallColor;
        GetComponentInChildren<ParticleSystem>().startColor = new Color(startBallColor.r, startBallColor.g, startBallColor.b, 0.5f);
        smoke2.startColor = new Color(startBallColor.r, startBallColor.g, startBallColor.b, 0.5f);
    }
    public void MakeGlow()
    {
        Instantiate(glowPrefab, transform.position, Quaternion.identity, this.gameObject.transform);
    }

    public void Resurrection()
    {
        rb.useGravity = true;
        gameObject.GetComponent<JellyMesh>().Intensity = 0;
        gameObject.GetComponentInChildren<TrailRenderer>().time = 0;
        transform.position = new Vector3(startPos.x, transform.position.y + 6, startPos.z);
        Camera.main.transform.position = new Vector3(0, transform.position.y + 8, -9.5f);
        GameManager.singleton.isAlive = true;
        rb.gameObject.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        AllowCollision();
    }
    public void Jump()
    {
        if (GameManager.singleton.isAlive && rb.velocity.y == 0)
        {
            ignoreNextCollision = true;
            rb.AddForce(Vector3.up * ImpulseForce, ForceMode.Impulse);
            AudioManage.ResetPitch(1.25f);
            AudioManage.PlaySound(1);
            Invoke("AllowCollision", .1f);
        }
    }
}
