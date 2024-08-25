using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    public enum EnemyType
    {
        A,
        B,
        C
    }

    public EnemyType enemyType;
    public Transform transforms;
    Animator animator;
    float hitTimer;
    bool bCanHit;
    float fireTimer;
    bool bCanFire;
    public GameObject explosion;
    public GameObject[] items;
    public GameObject bullet;

    public GameObject recentlyHitShooter;
    // Start is called before the first frame update
    void Start()
    {
        bCanFire = true;
        bCanHit = true;
        animator = GetComponent<Animator>();
        StartCoroutine(CoMove());
    }

    // Update is called once per frame
    void Update()
    {
    }

    public IEnumerator CoMove()
    {
        if (enemyType != EnemyType.C)
        {
            while (true)
            {
                if (enemyType == EnemyType.B)
                {
                    EnemyAttack();

                }
                if (bCanHit)
                {
                    transforms.Translate(new Vector3(0, -1f * Time.deltaTime, 0f));
                    if (transform.position.y < -5f)
                    {
                        Destroy(transform.parent.gameObject);
                    }
                }
              
                if (bCanHit == false && hitTimer > 0f)
                {
                    hitTimer -= Time.deltaTime;
                    if (hitTimer < 0f)
                    {
                        bCanHit = true;
                        Debug.Log("AA");
                        GameObject go = GameObject.Instantiate(explosion);
                        go.transform.position = transform.position;
                        if (recentlyHitShooter != null) recentlyHitShooter.GetComponent<ShooterController>().Score += 500;
                        int itemR = Random.Range(0, 3);
                        float r = Random.Range(0f, 100f);
                        if (r >= 80f)
                        {
                            GameObject bombGo = GameObject.Instantiate(items[itemR]);
                            bombGo.transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
                        }
                        Destroy(transform.parent.gameObject);
                    }
                }
                yield return null;
            }
        }
        else
        {
            while (true)
            {
                if (bCanHit == false && hitTimer > 0f)
                {
                    hitTimer -= Time.deltaTime;
                    if (hitTimer < 0f)
                    {
                        bCanHit = true;
                        animator.SetBool("hit", false);

                    }
                }
                yield return null;
            }
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
        if (bCanFire)
        {
            GameObject targetObject = GameObject.Find("Shooter");
            if (targetObject != null)
            {
                Vector2 dir = (targetObject.transform.position - transform.position).normalized;
                float len = (targetObject.transform.position - transform.position).magnitude;

                if (len < 6f)
                {
                    bCanFire = false;
                    fireTimer = 2f;
                    GameObject go = Instantiate(bullet);
                    go.GetComponent<BulletController>().dir = dir;
                    go.transform.position = new Vector3(transform.position.x, transform.position.y - 1f, 0f);
                }

            }
        }
    }
    public void Hit(GameObject b)
    {
        if (bCanHit)
        {
            if (b != null)
            {
                recentlyHitShooter = b;
                bCanHit = false;
                if (enemyType == EnemyType.C)
                {
                    b.GetComponent<ShooterController>().Score += 100;
                    animator.SetBool("hit", true);
                }
                hitTimer = 0.05f;
            }
        }
        else if(enemyType == EnemyType.C)
        {
            if (b != null)
            {
                b.GetComponent<ShooterController>().Score += 100;
            }
        }
    }
}
