using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int foodValue;

    private int score;
    void Start()
    {
            score = 0;
            UpdateScore();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (NewController.playing == true)
        {
            if(other.gameObject.tag == "Food") score += foodValue;
            else score -= foodValue;
            UpdateScore();
            Destroy(other.gameObject);
        }
    }

    void UpdateScore()
    {
        scoreText.text = "Score:\n" + score;   
    }

}
