using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 1f;
    [SerializeField] float maxJumpTime = 5f;
    [SerializeField] int playerLives = 3;
    [SerializeField] Vector2 deathKick = new Vector2(5f, 5f);
    [SerializeField] float jumpForce = 0.1f;
    Rigidbody2D myRigidBody;
    CapsuleCollider2D myBodyCollider;
    Animator myAnimator;
    BoxCollider2D feetCollider;
    float gravityScale;
    bool isAlive = true;
    float jumpStartTime;
    bool jumpCancel = false;
    bool isJumping = false;
    bool grounded;


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
        ClimbLadder();
        Die();

        CheckJumping();

        grounded = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));


    }

    private void FixedUpdate()
    {

        // Normal jump (full speed)
        if (isJumping)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed);
            isJumping = false;
        }
        // Cancel the jump when the button is no longer pressed
        if (jumpCancel)
        {
            if (myRigidBody.velocity.y > jumpSpeed * 0.5f)
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpSpeed * 0.5f);
            jumpCancel = false;
        }
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

    private void CheckJumping()
    {
        if (Input.GetButtonDown("Jump") && grounded) {
            isJumping = true;
        }

        if (Input.GetButtonUp("Jump") && !grounded)
        {
            jumpCancel = true;
        }
    }




 /*   private void Jump()
    {


        if (isJumping && !jumpKeyHeld)
        {
            jumpStartTime = Time.time;
            jumpKeyHeld = true;
            myRigidBody.velocity = new Vector2(0f, jumpSpeed);
        }

        else if (isJumping && jumpKeyHeld && (jumpStartTime + maxJumpTime) > Time.time)
        {
            myRigidBody.velocity = new Vector2(0f, jumpSpeed);
            Debug.Log(myRigidBody.velocity);
        }

    }

    private void JumpStuff()
    {


        if (isJumping && !jumpKeyHeld)
        {
            jumpStartTime = Time.time;
            jumpKeyHeld = true;
            myRigidBody.velocity = new Vector2(0f, jumpSpeed);
        }
        else if (isJumping && jumpKeyHeld && (jumpStartTime + maxJumpTime) > Time.time)
        {
            Debug.Log((jumpStartTime + maxJumpTime) > Time.time);
            myRigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

    }
*/


}
