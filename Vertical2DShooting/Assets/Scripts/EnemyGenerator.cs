using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    static float gameTimer;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    void Start()
    {
        StartCoroutine(GenerateWave());
    }
    void Update()
    {
    }

    IEnumerator GenerateWave()
    {
        while (true)
        {
            //1 Wave
            if (gameTimer == 0f)
            {
                for (int i = 6; i < 15; i++)
                {
                    GameObject go1 = Instantiate(enemy1);
                    go1.transform.position = new Vector3(-1.5f,i, 0);
                }
            }

            //2 Wave
            if (gameTimer == 8f)
            {
                GameObject go1 = Instantiate(enemy2);
                go1.transform.position = new Vector3(-2, 5f, 0);
                GameObject go2 = Instantiate(enemy2);
                go2.transform.position = new Vector3(0, 5f, 0);
                GameObject go3 = Instantiate(enemy2);
                go3.transform.position = new Vector3(2, 5f, 0);

            }

            //3 Wave
            if(gameTimer == 15f)
            {
                GameObject go1 = Instantiate(enemy3);
                go1.transform.position = new Vector3(0, 6f, 0);

                break;
            }
            
            yield return new WaitForSeconds(1f);
            gameTimer += 1f;
        }
       
        
    }
}
