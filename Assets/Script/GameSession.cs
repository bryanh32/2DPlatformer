using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int playerLives = 3;
    float score = 0f;


    private void Awake()
    {
        SetUpSingleton();
    }



    void Start()
    {
    }


    private void SetUpSingleton()
    {
        int numberOfGameSessions = FindObjectsOfType(GetType()).Length;

        if (numberOfGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void ResetGameSession()
    {
        FindObjectOfType<LevelLoader>().LoadMainMenu();
        Destroy(gameObject);
    }

    private void TakeLife()
    {
        playerLives--;
        FindObjectOfType<LevelLoader>().LoadCurrentLevel();
    }


    public void addToScore(float scoreAdd)
    {
        score += scoreAdd;
    }

    public float getScore()
    {
        return score;
    }

    public int getLives()
    {
        return playerLives;
    }

        

    
}
