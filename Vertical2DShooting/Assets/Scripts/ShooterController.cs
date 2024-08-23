using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{

    public int hp = 3;
    public float moveSpeed = 1f;
    public GameObject bullet;
    public GameObject bullet2;
    Animator animator;
    Rigidbody2D rigid;
    float fireTimer;
    bool bCanFire;
    // Start is called before the first frame update


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("item"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("enemy1"))
        {
            hp--;
        }
    }

    void Start()
    {
        bCanFire = true;
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
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
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        animator.SetInteger("direction", (int)h);

        Vector3 dir = new Vector3(h, v, 0); 

        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);



        if(Input.GetKey(KeyCode.Space))
        {
            if (bCanFire)
            {
                bCanFire = false;
                fireTimer = 0.1f;
                GameObject bullets = Instantiate(bullet);

                bullets.transform.position = new Vector2(transform.position.x, transform.position.y + 0.6f);
                GameObject bullets2 = Instantiate(bullet2);

                bullets2.transform.position = new Vector2(transform.position.x - 0.27f, transform.position.y + 0.6f);
                GameObject bullets3 = Instantiate(bullet2);

                bullets3.transform.position = new Vector2(transform.position.x + 0.27f, transform.position.y + 0.6f);
            }
        }

    }
}
