using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D myRigidBody;
    Animator myAnimator;

    public void Update()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * moveSpeed;

        myRigidBody.velocity = new Vector2(deltaX, myRigidBody.velocity.y);
        flipSprite();

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }

    private void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }
}
