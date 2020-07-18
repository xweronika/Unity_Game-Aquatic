using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject scoreText;
    public GameObject hiscoreText;

    // Start is called before the first frame update
    void Start()
    {



    }

    public void setScore(int score)
    {
        scoreText.GetComponent<Text>().text = score.ToString();
    }
    public void setHighscore(int score)
    {
        hiscoreText.GetComponent<Text>().text = score.ToString();
    }

}
