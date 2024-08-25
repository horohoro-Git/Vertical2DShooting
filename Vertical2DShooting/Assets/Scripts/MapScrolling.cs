using UnityEngine;
public class MapScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] transforms;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transforms.Length; i++)
        {
            transforms[i].position += new Vector3(0, -speed, 0) * Time.deltaTime;

            if (transforms[i].position.y <= -11f)
            {
               
                transforms[i].position = new Vector3(0,18f,0);
            }
        }
    }
}
