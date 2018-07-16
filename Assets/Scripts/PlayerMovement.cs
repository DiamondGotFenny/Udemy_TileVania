using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

    //config
    [SerializeField]
    float moveSpeed = 5f;
    [SerializeField]
    float jumpSpeed = 5f;
    [SerializeField]
    float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(50f, 50f);
    float gravityScaleStart;

    //state
    bool alive = true;

    //cached component reference
    Rigidbody2D myrigidbody2D;
    Animator myAnimator;
    BoxCollider2D myFeetCollider2D;
    CapsuleCollider2D myBodyCollider2D;
    Menu myMenu;

	// Use this for initialization
	void Start () {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myFeetCollider2D = GetComponent<BoxCollider2D>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleStart = myrigidbody2D.gravityScale;
        myMenu = FindObjectOfType<Menu>().GetComponent<Menu>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!alive)
        {
            return;
        }
        Run();
        FlipSprite();
        Climb();
        Die();
        Jump();  // don't put this method in FixedUpdate(), it will have problem;        
    }

    private void Run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 runVelocity = new Vector2(controlThrow*moveSpeed, myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = runVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody2D.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    private void Jump()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) //if the player not touch the ground
        {
            return; // we do nothing;
        }

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            myrigidbody2D.velocity += jumpVelocityToAdd;
        }
    }

    private void Climb()
    {
        if (!myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))) //if the player not touch the Ladder;
        {
            myAnimator.SetBool("IsClimbing", false);
            myrigidbody2D.gravityScale = gravityScaleStart;
            return;
        }

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Vector2 climbVelocity = new Vector2(myrigidbody2D.velocity.x, controlThrow * climbSpeed);
        myrigidbody2D.velocity = climbVelocity;
        myrigidbody2D.gravityScale = 0f;

        bool playerHasVerticalSpeed = Mathf.Abs(myrigidbody2D.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
    }
    
    public void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            alive = false;
            myAnimator.SetTrigger("Die");
            myrigidbody2D.velocity = deathKick;
            FindObjectOfType<GameSession>().TakeLives();
            Invoke("DeathReload", 3f);
        }
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myrigidbody2D.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myrigidbody2D.velocity.x), 1f);
        }
    }

    void DeathReload()
    {
        myMenu.ReLoadLevel();
    }
}
