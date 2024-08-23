using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectLifeSpan : MonoBehaviour
{
    public float lifeSpan; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeSpan -= Time.deltaTime;
        if (lifeSpan < 0f) Destroy(gameObject);
    }
}
