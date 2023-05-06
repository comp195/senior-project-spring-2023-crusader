using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    private bool isFacingRight;

    private bool canRoll = true;
    private bool isRolling;
    private float rollingCooldown = 1f;
    private float rollingPower = 24f;
    private float rollingTime = 0.5f;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    public Transform attackPoint;
    public Animator animator;

    // Update is called once per frame

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 4f;
        jumpForce = 60f;
        isJumping = false;
        isFacingRight = true;
    }

    void Update()
    {

        if(isRolling)
            return;

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(KeyCode.LeftShift) && canRoll)
        {
            StartCoroutine(Roll());
        }
    }

    void FixedUpdate()
    {
        if(isRolling)
            return;
        if(moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            animator.SetBool("isRunning", true);
        } else 
        {
            animator.SetBool("isRunning", false);
        }

         if(!isJumping && moveVertical > 0.1f)
        {
            rb.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }

        if (moveHorizontal > 0.1f && !isFacingRight)
			{
				Flip();
			}
			else if (moveHorizontal < -0.1f && isFacingRight)
			{
				// ... flip the player.
				Flip();
			}
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.gameObject.tag == "Platform")
        {
            isJumping = true;
            animator.SetBool("isJumping", true);
        }

        
    }

    private void Flip()
	{
		// Switch the way the player is labelled as facing.
		isFacingRight = !isFacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

    private IEnumerator Roll()
    {
        canRoll = false;
        isRolling = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * rollingPower, 0f);
        tr.emitting = true;
        animator.SetTrigger("Roll");
        yield return new WaitForSeconds(rollingTime);
        animator.SetTrigger("Idle");
        tr.emitting=false;
        rb.gravityScale=originalGravity;
        isRolling = false;
        yield return new WaitForSeconds(rollingCooldown);
        canRoll = true;
        Debug.Log("hello");
    }
}

//Credit for Movement Implementation: https://www.youtube.com/watch?v=w9NmPShzPpE
//Credit for Animation Implementation: https://www.youtube.com/watch?v=hkaysu1Z-N8
