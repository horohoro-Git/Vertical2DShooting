using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    
    ShooterController controller;
    public Text text1;
    public Image life;
    public Text text_Score;
    public List<Image> lives = new List<Image>();
    // Start is called before the first frame update
    void Start()
    {
        controller = GameObject.Find("Shooter").GetComponent<ShooterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        text1.text = $" X {controller.ps.bomb}";
    }

    public void UpdateLifePoint(bool bIncrease)
    {
        if (bIncrease)
        {
            int cnt = lives.Count;
            Image lifeImage = GameObject.Instantiate<Image>(life);
            lifeImage.transform.SetParent(this.gameObject.transform);
            lifeImage.rectTransform.position = new Vector3(50f * (cnt + 1), 1030f, 0f);
            lives.Add(lifeImage);
        }
        else
        {
            Image destroyImage = lives[lives.Count - 1];
            lives.Remove(destroyImage);
            Destroy(destroyImage.gameObject);
        }
    }

    public void UpdateScorePoint(int score)
    {
        text_Score.text = $"{score : #,###}";
    }
}
