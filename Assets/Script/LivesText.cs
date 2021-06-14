using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesText : MonoBehaviour
{
    Text livesText;
    // Update is called once per frame
    void Update()
    {
        livesText = GetComponent<Text>();
        livesText.text = FindObjectOfType<GameSession>().getLives().ToString();
    }
}
