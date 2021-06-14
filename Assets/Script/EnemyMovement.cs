using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myRigidBody;
    [SerializeField] float moveSpeed = 1f;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void Movement()
    {
        if (IsFacingRight())
        {
            myRigidBody.velocity = new Vector2(moveSpeed, 0f);
        }
        else
        {
            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    public bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
}
