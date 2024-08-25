using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour
{
    public GameObject parentGO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            enemy.Hit(parentGO);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
