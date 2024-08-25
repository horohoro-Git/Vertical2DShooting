using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public enum BulletType
    {
        BULEET,
        BOMB,
        EnemyBullet,
        Guided,
        None
    }
    public float lifeSpan = 4f;
    public float bulletSpeed = 10f;
    public BulletType type;
    public GameObject hitObject;
    public Vector3 dir;
    public GameObject parentGO;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            switch (type)
            {
                case BulletType.BULEET:
                    {
                        EnemyController enemy = collision.GetComponent<EnemyController>();
                        enemy.Hit(parentGO);
                        break;
                    }
                case BulletType.BOMB:
                    {
                        Explosion();
                        break;
                    }
            }

            Destroy(this.gameObject);

        }
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ShooterController>().Hit();
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if(lifeSpan <0f)
        {
            if(type == BulletType.BOMB)
            {
                Explosion();
            }
            Destroy(this.gameObject);
        }


        if (type == BulletType.EnemyBullet) transform.Translate(dir * bulletSpeed * Time.deltaTime);
        else if(type == BulletType.BULEET || type==BulletType.BOMB) transform.Translate(new Vector3(0, bulletSpeed * Time.deltaTime, 0));
        else if (type == BulletType.Guided)
        {

            transform.Translate(dir * bulletSpeed * Time.deltaTime);
            bulletSpeed -= Time.deltaTime * 4f;
            if (bulletSpeed < 0f)
            {
                type = BulletType.None;
                StartCoroutine(GuidedMissile());
            }
        }
    }


    void Explosion()
    {
        GameObject explosionGO = Instantiate(hitObject);
        explosionGO.GetComponent<SplashDamage>().parentGO = parentGO;
        explosionGO.transform.position = transform.position;


    }


    IEnumerator GuidedMissile()
    {
        GameObject gameObject = GameObject.Find("Shooter");
        Vector3 targetTransform = new Vector2();
        if (gameObject != null)
        {
            targetTransform = gameObject.transform.position;
        }

        yield return new WaitForSeconds(1.5f);


        Vector2 direction =  targetTransform - transform.position;
        dir = direction.normalized;
        bulletSpeed = 50f;
        type = BulletType.EnemyBullet;
    }
}
