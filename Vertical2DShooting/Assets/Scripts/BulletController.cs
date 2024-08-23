using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifeSpan = 4f;
    public float bulletSpeed = 10f;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("enemy1"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            enemy.Hit();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <0f)
        {
            Destroy(this.gameObject);
        }


        transform.Translate(new Vector3(0, bulletSpeed * Time.deltaTime, 0));

    }
}
