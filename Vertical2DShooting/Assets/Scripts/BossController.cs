using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator animator;
    float hitTimer;
    bool bCanHit;
    float fireTimer;
    bool bCanFire;
    public GameObject bullet;
    public GameObject bullet2;
    public GameObject bullet3;
    // Start is called before the first frame update
    void Start()
    {
        bCanFire = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (transform.position.y > 3f)
        {
            transform.Translate(new Vector3(0, -1f * Time.deltaTime, 0f));
        }
        else
        {
            EnemyAttack();
        }
        if (bCanHit == false)
        {
            hitTimer -= Time.deltaTime;
            if(hitTimer < 0f)
            {
                bCanHit = true;
                animator.SetBool("hit", false);
            }
        }


    }
    public void Hit()
    {
        if (bCanHit)
        {
            bCanHit = false;
            animator.SetBool("hit", true);
            hitTimer = 0.1f;
        }
    }
    public void EnemyAttack()
    {
        if (fireTimer > 0f && bCanFire == false)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer < 0f)
            {
                bCanFire = true;
            }
        }
        if(bCanFire)
        {
            GameObject targetObject = GameObject.Find("Shooter");
            if (targetObject != null)
            {
                float len = (targetObject.transform.position - transform.position).magnitude;

                if (len < 15f)
                {
                    bCanFire = false;
                    int rand = Random.Range(1, 5);
                   
                    switch (rand)
                    {
                        case 1:
                            {
                                fireTimer = 2f;

                                for (int i = -1; i < 2; i++)
                                {
                                    Vector3 temp1 = new Vector3(targetObject.transform.position.x + i, targetObject.transform.position.y, 0);
                                    Vector2 temp1Dir = (temp1 - transform.position).normalized;
                                    // Vector2 dir = (targetObject.transform.position - transform.position).normalized;
                                    GameObject go = Instantiate(bullet);
                                    BulletController bc = go.GetComponent<BulletController>();
                                    bc.dir = temp1Dir;

                                    go.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
                                }
                            }


                            break;

                        case 2:
                            {
                                fireTimer = 4f;

                                GameObject l = Instantiate(bullet2);
                                l.GetComponent<BulletController>().dir = Vector2.left;
                                l.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                                GameObject r = Instantiate(bullet2);
                                r.GetComponent<BulletController>().dir = Vector2.right;
                                r.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                                 GameObject u = Instantiate(bullet2);
                                u.GetComponent<BulletController>().dir = Vector2.up;
                                u.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                                 GameObject d = Instantiate(bullet2);
                                d.GetComponent<BulletController>().dir = Vector2.down;
                                d.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

                                for (int j = 0; j <= 1; j++)
                                {
                                    int i = 1;
                                    if (j == 1)
                                    {
                                        i = -1;
                                    }
                                    for (float k = 0.25f; k <= 0.75f; k += 0.25f)
                                    {
                                        Vector3 temp1 = new Vector3(i, k, 0);
                                        Vector2 temp1Dir = temp1.normalized;
                                        GameObject go1 = Instantiate(bullet2);
                                        go1.GetComponent<BulletController>().dir = temp1Dir;

                                        go1.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

                                        Vector3 temp2 = new Vector3(i, -k, 0);
                                        Vector2 temp2Dir = temp2.normalized;
                                        GameObject go2 = Instantiate(bullet2);
                                        go2.GetComponent<BulletController>().dir = temp2Dir;

                                        go2.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

                                        Vector3 temp3 = new Vector3(k, i, 0);
                                        Vector2 temp3Dir = temp3.normalized;
                                        GameObject go3 = Instantiate(bullet2);
                                        go3.GetComponent<BulletController>().dir = temp3Dir;

                                        go3.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

                                        Vector3 temp4 = new Vector3(-k, i, 0);
                                        Vector2 temp4Dir = temp4.normalized;
                                        GameObject go4 = Instantiate(bullet2);
                                        go4.GetComponent<BulletController>().dir = temp4Dir;

                                        go4.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                                    }
                                }


                                break;
                            }
                        case 3:
                            {
                                fireTimer = 7f;

                                StartCoroutine(SpreadAttack());
                                break;
                            }
                        case 4: 
                            {
                                fireTimer = 5f;
                                StartCoroutine(GuidedMissile());
                                break;
                            }
                    }
                }
            }

        }
    }

    IEnumerator SpreadAttack()
    {
        for (float i = 1; i >= -1; i -= 0.2f)
        {
            GameObject l = Instantiate(bullet2);
            l.GetComponent<BulletController>().dir = new Vector2(i, -1).normalized;

            l.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);


            yield return new WaitForSeconds(0.2f);
        }

        for (float i = -1; i <= 1; i += 0.2f)
        {
            GameObject l = Instantiate(bullet2);
            l.GetComponent<BulletController>().dir = new Vector2(i, -1).normalized;

            l.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

            yield return new WaitForSeconds(0.2f);
        }
    }
    IEnumerator GuidedMissile()
    {
        GameObject b1 = Instantiate(bullet3);
        b1.GetComponent<BulletController>().dir = new Vector2(-0.8f, 1).normalized;

        b1.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        yield return new WaitForSeconds(0.5f);

        GameObject b2 = Instantiate(bullet3);
        b2.GetComponent<BulletController>().dir = new Vector2(-2f, 1).normalized;

        b2.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        yield return new WaitForSeconds(0.5f);
        GameObject b3 = Instantiate(bullet3);
        b3.GetComponent<BulletController>().dir = new Vector2(0.8f, 1).normalized;

        b3.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        yield return new WaitForSeconds(0.5f);
        GameObject b4 = Instantiate(bullet3);
        b4.GetComponent<BulletController>().dir = new Vector2(2f, 1).normalized;

        b4.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

    }
}
