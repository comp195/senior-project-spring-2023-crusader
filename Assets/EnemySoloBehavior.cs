using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
public class EnemySoloBehavior : MonoBehaviour
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
    private float count = 0;
    void Start () {
        currentHealth = maxHealth;
    }

    void Update () {
        if(Vector2.Distance(transform.position, player.position) < 50){
            Debug.Log(attacking);
            anim.SetBool("hit",false);
            if (Vector2.Distance(transform.position, player.position) <= attackRange && !attacking) {
                anim.SetBool("canAttack", true);
                anim.SetBool("canWalk", false);
                Attack();
            } else {
                Debug.Log("walking");
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
            Debug.Log("hello");
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
        if (Vector2.Distance(transform.position, player.position) <= attackRange) {
            yield return new WaitForSeconds(1.5f);
            player.GetComponent<PlayerBehavior>().PlayerTakeDamage(attackDamage);
            Debug.Log("attack");
            yield return new WaitForSeconds(attackCooldown);
        }
        Debug.Log("exit" + count++);
        attacking = false;
        anim.SetBool("canWalk", true);
        anim.SetBool("canAttack", false);
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
