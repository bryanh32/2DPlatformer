using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.2f;
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector2(0f, scrollSpeed * Time.deltaTime));
    }
}
