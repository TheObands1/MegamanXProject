using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] float dashJumpSpeedMultiplier;
    [SerializeField] float dashSpeed;
    [SerializeField] BoxCollider2D FeetCollider;
    [SerializeField] Sprite IdleSprite;
    [SerializeField] Sprite FallingSprite;
    [SerializeField] float dashTime;
    [SerializeField] float StartDashTime;
    float NormalJumpSpeed;
    float DashingJumpSpeed;
    float lastPressedFrame;
    bool canDoubleJump;
    int jumpCounter;
    
    Animator myAnimator;
    SpriteRenderer myRenderer;
    Rigidbody2D myRigidBody2D;
    BoxCollider2D myMainCollider;
    


    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRenderer = GetComponent<SpriteRenderer>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myMainCollider = GetComponent<BoxCollider2D>();

        dashTime = StartDashTime;
        NormalJumpSpeed = jumpSpeed;
        DashingJumpSpeed = jumpSpeed * dashJumpSpeedMultiplier;
        lastPressedFrame = 0;

        //StartCoroutine(ShowTime());
    }

    // Update is called once per frame
    void Update()
    {
       Movement();
       Jump();
       CharacterFallingDetector();
       Fire();
       Dash();
       //Debug.Log("jumpspeedvalue " + jumpSpeed);
       //Debug.Log("lastPressed " + lastPressedFrame + " time " + Time.deltaTime);

    }
    /*
    IEnumerator ShowTime()
    {
        int count = 0;
        while(true)
        {
            yield return new WaitForSeconds(1f);
            count++;
            Debug.Log("Time: " + count);
        }
    }
    */
    void Movement()
    {
        float CurrentMovement = Input.GetAxis("Horizontal");
        if (CurrentMovement != 0)
        {
            myAnimator.SetBool("isRunning", true);
            transform.localScale = new Vector2(Mathf.Sign(CurrentMovement), 1);
            transform.Translate(new Vector2(CurrentMovement * speed * Time.deltaTime, 0));
          
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {

        if (IsTouchingTheGround() && !myAnimator.GetBool("IsJumping"))
        {
            myAnimator.SetBool("isFalling", false);
            myAnimator.SetBool("IsJumping", false);

            if (Input.GetKeyDown(KeyCode.Space) && jumpCounter == 0)
            {
                jumpSpeed = 20;
                myAnimator.SetTrigger("JumpStartTrigger");
                myAnimator.SetBool("IsJumping", true);
                myRigidBody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                jumpCounter = 1;
                
            }
        }
        if(!myAnimator.GetBool("isFalling"))
        {
            if (Input.GetKeyDown(KeyCode.Space)&& jumpCounter <= 1)
            {
                jumpSpeed = jumpSpeed / 3;
                myAnimator.SetTrigger("JumpStartTrigger");
                myAnimator.SetBool("IsJumping", true);
                myRigidBody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
                //canDoubleJump = false;
                jumpCounter = 2;
            }
        }
        else
        {
           myAnimator.SetBool("isFalling", true);
        }
        
    }

    bool IsTouchingTheGround()
    {
        /*return FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));*/

        jumpCounter = 0;
        RaycastHit2D GroundRaycast = Physics2D.Raycast(myMainCollider.bounds.center, Vector2.down, myMainCollider.bounds.extents.y + 0.35f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myMainCollider.bounds.center, new Vector2(0, (myMainCollider.bounds.extents.y + 0.35f) * -1), Color.red);

        return GroundRaycast.collider != null;
        

    }

    void AfterJumpStartEvent()
    {
        myAnimator.SetBool("IsJumping", false);
        myAnimator.SetBool("isFalling", true);
    }

    void CharacterFallingDetector()
    {
        if(myRigidBody2D.velocity.y < 0 && !myAnimator.GetBool("IsJumping") && !myAnimator.GetBool("IsDashing"))
        {
            myAnimator.SetBool("isFalling", true);
        }
    }

    void Fire()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else if(Input.GetKeyUp(KeyCode.X))
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    void Dash()
    {
        if(!myAnimator.GetBool("IsJumping") && !myAnimator.GetBool("isFalling") && !myAnimator.GetBool("IsDashing"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(dashTime <= 0)
                {
                    dashTime = StartDashTime;
                    myRigidBody2D.velocity = Vector2.zero;
                    myAnimator.SetBool("IsDashing", false);
                }
                else
                {
                    lastPressedFrame = Time.deltaTime;
                    StartCoroutine(ChangeJumpSpeed());
                    dashTime -= Time.deltaTime;
                    myAnimator.SetBool("IsDashing", true);
                    if (transform.localScale.x == 1)
                    {
                        myRigidBody2D.velocity = Vector2.right * dashSpeed;
                    }
                    else if(transform.localScale.x == -1)
                    {
                        myRigidBody2D.velocity = Vector2.left * dashSpeed;
                    }
                }
            }
        }
        else
        {
            myAnimator.SetBool("IsDashing", false);
        }
    }

    IEnumerator ChangeJumpSpeed()
    {
        jumpSpeed = DashingJumpSpeed;
        yield return new WaitForSeconds(1f);
        jumpSpeed = NormalJumpSpeed;
    }
    
}
