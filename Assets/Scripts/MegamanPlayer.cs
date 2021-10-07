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
    [SerializeField] GameObject bullet,bullet2;
    float NormalJumpSpeed;
    float DashingJumpSpeed;
    bool canDoubleJump;
    
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

    }

    // Update is called once per frame
    void Update()
    {
       Movement();
       Jump();
       CharacterFallingDetector();
       Fire();
       Dash();
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

    public Vector3 getScale()
    {
        return transform.localScale;
    }

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
            canDoubleJump = false;

            if (Input.GetKeyDown(KeyCode.Space) && !canDoubleJump)
            {
                canDoubleJump = true;
                myAnimator.SetTrigger("JumpStartTrigger");
                myAnimator.SetBool("IsJumping", true);
                myRigidBody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }

        if(myAnimator.GetBool("isFalling"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && canDoubleJump)
            {
                float NewjumpSpeed = jumpSpeed / 1.25f;
                myAnimator.SetTrigger("JumpStartTrigger");
                myAnimator.SetBool("IsJumping", true);
                myRigidBody2D.AddForce(new Vector2(0, NewjumpSpeed), ForceMode2D.Impulse);
                canDoubleJump = false;
            }
        }
    }

    bool IsTouchingTheGround()
    {
        /*return FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));*/
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
            if(transform.localScale==new Vector3(1,1,0)||transform.localScale==new Vector3(1,1,1))
            {
                Instantiate(bullet, transform.position - new Vector3(0, 0, 0), transform.rotation);
            }
            if(transform.localScale==new Vector3(-1,1,0))
            {
                Instantiate(bullet2, transform.position - new Vector3(0, 0, 0), transform.rotation);
            }
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
