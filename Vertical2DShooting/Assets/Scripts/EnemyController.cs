using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform transforms;
    Animator animator;
    float hitTimer;
    bool bCanHit;
    public GameObject explosion;
    public GameObject bomb;
    // Start is called before the first frame update
    void Start()
    {
        bCanHit = true;
        animator = GetComponent<Animator>();

        //   Color color = new Color(1, 0, 0, 1);
        //   reder.color = color;
        StartCoroutine(CoFade());
        StartCoroutine(CoMove());
    }

    IEnumerator CoFade()
    {
        SpriteRenderer reder = GetComponent<SpriteRenderer>();

        for (float i = 1f; i >= 0; i -= 0.1f)
        {
            Color color = new Color(1, 1, 1, i);
            reder.color = color;

            yield return new WaitForSeconds(1f);
        }

    }

    // Update is called once per frame
    void Update()
    {
       /* if(bCanHit)
        {
            transforms.Translate(new Vector3(1f* Time.deltaTime, 1 * Time.deltaTime, 0));
            transforms.Rotate(new Vector3(0, 0, 0.1f));
            transform.Rotate(new Vector3(0, 0, -0.1f));
        }

        if (bCanHit == false && hitTimer > 0f)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0f)
            {
                //bCanHit = true;

                GameObject go = GameObject.Instantiate(explosion);
                go.transform.position = transform.position;

                int r = Random.Range(1, 5);
                if(r == 1)
                {
                    GameObject bombGo = GameObject.Instantiate(bomb);
                    bombGo.transform.position = transform.position;
                }
                Destroy(gameObject);
            }
        }*/


    }

    public IEnumerator CoMove()
    {
        while (true)
        {
            if (bCanHit)
            {
                transforms.Translate(new Vector3(1f * Time.deltaTime, 1 * Time.deltaTime, 0));
                transforms.Rotate(new Vector3(0, 0, 0.1f));
                transform.Rotate(new Vector3(0, 0, -0.1f));
            }

            if (bCanHit == false && hitTimer > 0f)
            {
                hitTimer -= Time.deltaTime;
                if (hitTimer < 0f)
                {
                    //bCanHit = true;

                    GameObject go = GameObject.Instantiate(explosion);
                    go.transform.position = transform.position;

                    int r = Random.Range(1, 1);
                    if (r == 1)
                    {
                        GameObject bombGo = GameObject.Instantiate(bomb);
                        bombGo.transform.position = transform.position;
                    }
                    Destroy(gameObject);
                }
            }
            yield return null;
        }
       
    }
    public void Hit()
    {
        if (bCanHit)
        {
            StartCoroutine(CoFade());
            bCanHit = false;
           // animator.SetBool("hit", true);
            hitTimer = 0.05f;
        }
    }
}
