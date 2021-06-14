using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    Text scoreText;
    // Update is called once per frame
    void Update()
    {
        scoreText = GetComponent<Text>();
        scoreText.text = FindObjectOfType<GameSession>().getScore().ToString();
    }
}
