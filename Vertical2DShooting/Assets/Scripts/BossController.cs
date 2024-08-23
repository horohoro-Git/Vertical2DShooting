using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    Animator animator;
    float hitTimer;
    bool bCanHit;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(bCanHit == false)
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
}
