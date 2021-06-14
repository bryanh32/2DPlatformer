using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(2f);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
