using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegamanPlayer : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    [SerializeField] BoxCollider2D FeetCollider;
    [SerializeField] Sprite IdleSprite;
    [SerializeField] Sprite FallingSprite;

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
    }

    // Update is called once per frame
    void Update()
    {
       Movement();
       Jump();
       CharacterFallingDetector();
       Fire();
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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myAnimator.SetTrigger("JumpStartTrigger");
                myAnimator.SetBool("IsJumping", true);
                myRigidBody2D.AddForce(new Vector2(0, jumpSpeed), ForceMode2D.Impulse);
            }
        }
        else
        {
           // myAnimator.SetBool("isFalling", true);
        }
        
    }

    bool IsTouchingTheGround()
    {
        //return FeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));

        RaycastHit2D GroundRaycast = Physics2D.Raycast(myMainCollider.bounds.center, Vector2.down, myMainCollider.bounds.extents.y + 0.2f, LayerMask.GetMask("Ground"));
        Debug.DrawRay(myMainCollider.bounds.center, new Vector2(0, (myMainCollider.bounds.extents.y + 0.2f) * -1), Color.red);

        return GroundRaycast.collider != null;

    }

    void AfterJumpStartEvent()
    {
        myAnimator.SetBool("IsJumping", false);
        myAnimator.SetBool("isFalling", true);
    }

    void CharacterFallingDetector()
    {
        if(myRigidBody2D.velocity.y < 0 && !myAnimator.GetBool("IsJumping"))
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
}
