using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class EnemyBehavior : MonoBehaviour
{
    public Transform leftLimit;
    public Transform rightLimit;
    public Transform player;
    public float moveSpeed;
    public float attackRange;
    public int attackDamage;
    public float attackCooldown;
    public int maxHealth;
    private int currentHealth;
    private bool attacking = false;
    public Animator anim;
    private bool isFacingRight;
    private float moveHorizontal;

    void Start () {
        currentHealth = maxHealth;
        isFacingRight = false;
    }

    void U()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
    }
            
    void FixedUpdate()
    {
        if (player.transform.position.x < gameObject.transform.position.x && isFacingRight)
        {
            Flip();
        }
        else if (player.transform.position.x > gameObject.transform.position.x && !isFacingRight)
        {
            Flip();
        }
    }

    





private void Flip()
    {
        // Switch the way the player is labelled as facing.
        isFacingRight = !isFacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = gameObject.transform.localScale;
        theScale.x *= -1;
        gameObject.transform.localScale = theScale;
        
    }

    void Update () {
        moveHorizontal = Input.GetAxisRaw("Horizontal");

        if (Vector2.Distance(transform.position, player.position) < 50){
            anim.SetBool("hit",false);
            if (Vector2.Distance(transform.position, player.position) <= attackRange && !attacking) {
                anim.SetBool("canAttack", true);
                Attack();
            } else {
                anim.SetBool("canWalk", true);
                anim.SetBool("canAttack", false);
                if (player.position.x < transform.position.x) {
                    transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);
                    
                } else {
                    transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
                    
                }
                if (transform.position.x > rightLimit.position.x) {
                    transform.position = new Vector2(rightLimit.position.x, transform.position.y);
                    
                } else if (transform.position.x < leftLimit.position.x) {
                    transform.position = new Vector2(leftLimit.position.x, transform.position.y);
                    
                }
            }

        } else {
            anim.SetTrigger("idle");
        }
    }

    void Attack() {
        if (!attacking) {
            attacking = true;
            StartCoroutine(AttackPlayer());
        }
    }

    IEnumerator AttackPlayer() {
        while (Vector2.Distance(transform.position, player.position) <= attackRange) {
            player.GetComponent<PlayerBehavior>().PlayerTakeDamage(attackDamage);
            yield return new WaitForSeconds(attackCooldown);
        }
        attacking = false;
        anim.SetBool("canWalk", true);
    }
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        anim.SetBool("hit", true);
        Debug.Log(currentHealth + " enemy");
        if (currentHealth <= 0) {
            Die();
        }
    }

    void Die() {
        // add death animation and destroy object
        anim.SetBool("canWalk", false);
        anim.SetBool("die", true);
        GetComponent<EnemyBehavior>().enabled = false;
    }

}
