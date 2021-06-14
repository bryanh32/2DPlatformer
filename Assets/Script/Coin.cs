using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    [SerializeField] AudioClip coinSfx;
    [SerializeField] float coinScore = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinSfx, Camera.main.transform.position);
        FindObjectOfType<GameSession>().addToScore(coinScore);
        Destroy(gameObject);
    }
}
