using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddItem : MonoBehaviour
{
    public enum ItemType
    {
        BOMB,
        COIN,
        POWER
    
    }

    public ItemType type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ShooterController shooter = collision.gameObject.GetComponent<ShooterController>();
            switch(type)
            {
                case ItemType.BOMB:
                    {
                        shooter.ps.bomb++;
                        break;
                    }
                case ItemType.COIN:
                    {
                        shooter.ps.coin++;
                        break;
                    }
                case ItemType.POWER:
                    {
                        shooter.ps.power++;
                        break;
                    }
            }

            Destroy(this.gameObject);
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      //  transform.Rotate(new Vector3(1, 0, 0));
        transform.Translate(new Vector3(0, -0.8f * Time.deltaTime, 0));
        if(transform.position.y <= -6f) Destroy(this.gameObject);
    }
}
