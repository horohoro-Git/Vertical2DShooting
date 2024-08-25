using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    //UIManager uiWidget;
    public struct PlayerState
    {
        public int power { get; set; }
        public int coin { get; set; }
        public int bomb {  get; set; }
    }
    int life;
    Action<bool> lifeAction;
    Action<int> scoreAction;
    public int startLifePoint = 3;
    public UIManager uiWidget;
    public PlayerState ps;
    public float moveSpeed = 1f;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject powerBullet;
    public GameObject explosion;
    Animator animator;
    Rigidbody2D rigid;
    float fireTimer;
    bool bCanFire;
    int score;
    public int Score
    {
        get { return score; }
        set { score = value; 
              scoreAction(score);
             }
    }

    public int Life
    {
        get { return life; }
        set
        {
            bool bIncrease = Life > value ? false : true;
            life = value;
            lifeAction(bIncrease);
        }
    }
   
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Life--;
        }

    }

    void Start()
    {
        bCanFire = true;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        uiWidget = GameObject.Find("ui").GetComponent<UIManager>();
        lifeAction = (increase) => { uiWidget.UpdateLifePoint(increase); };
        scoreAction = (scores) => { uiWidget.UpdateScorePoint(scores); };
        for (int i = 0; i < startLifePoint; i++)
        {
            Life++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fireTimer > 0f && bCanFire == false)
        {
            fireTimer -= Time.deltaTime;
            if(fireTimer < 0f)
            {
                bCanFire = true;
            }
        }
        InputManagement();
       
    }
    public void Hit()
    {
        if(Life > 0) Life--;
        if(Life ==0)
        {
            GameObject go = Instantiate(explosion);
            go.transform.position = transform.position;
            GameObject.Destroy(gameObject);
        }
    }


    void InputManagement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        animator.SetInteger("direction", (int)h);

        Vector3 dir = new Vector3(h, v, 0);

        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);

        float newX = Mathf.Clamp(transform.position.x, -2.5f, 2.5f);
        float newY = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);

        transform.position = new Vector3(newX, newY, 0);

        if (Input.GetKey(KeyCode.Space))
        {
            if (bCanFire)
            {
                bCanFire = false;
                fireTimer = 0.2f;
                GameObject bullets = Instantiate(bullet);
                bullets.GetComponent<BulletController>().parentGO = this.gameObject;
                bullets.transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f);
                GameObject bullets2 = Instantiate(bullet2);
                bullets2.GetComponent<BulletController>().parentGO = this.gameObject;
                bullets2.transform.position = new Vector2(transform.position.x - 0.27f, transform.position.y + 0.6f);
                GameObject bullets3 = Instantiate(bullet2);
                bullets3.GetComponent<BulletController>().parentGO = this.gameObject;
                bullets3.transform.position = new Vector2(transform.position.x + 0.27f, transform.position.y + 0.6f);
            }
        }

        if (ps.bomb > 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                if (bCanFire)
                {
                    bCanFire = false;
                    fireTimer = 1f;
                    ps.bomb--;
                    GameObject pBullet = Instantiate(powerBullet);
                    pBullet.GetComponent<BulletController>().parentGO = this.gameObject;
                    pBullet.transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f);
                }
            }
        }
    }
}
