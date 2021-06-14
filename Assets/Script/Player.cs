using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float jumpTime = 0f;
    [SerializeField] int playerLives = 3;
    [SerializeField] Vector2 deathKick = new Vector2(5f, 5f);
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    Animator myAnimator;
    BoxCollider2D feetCollider;
    float gravityScale;
    bool isAlive = true;


    private void Start()
    {
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        feetCollider = GetComponent<BoxCollider2D>();
        gravityScale = myRigidBody.gravityScale;
    }
    public void Update()
    {
        if (!isAlive) {
            return; 
        }
       
        Move();
        Jump();
        ClimbLadder();
        Die();

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * moveSpeed;

        myRigidBody.velocity = new Vector2(deltaX, myRigidBody.velocity.y);
        flipSprite();

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", playerHasHorizontalSpeed);
    }


    private void ClimbLadder()
    {
        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("Climbing", false);
            myRigidBody.gravityScale = gravityScale;
            return;
        }

        myRigidBody.gravityScale = 0f;
        var deltaY = Input.GetAxis("Vertical") * moveSpeed;
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, deltaY);
        myAnimator.SetBool("Climbing", true);

    }


    private void Jump()
    {


        if (!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            StartJump();
        }
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("Die");
            GetComponent<Rigidbody2D>().velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void flipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
    }


    private void StartJump()
    {
        Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
        myRigidBody.velocity = jumpVelocityToAdd;

    }

    private void StopJump()
    {
        myRigidBody.gravityScale = 1f;
    }

}
