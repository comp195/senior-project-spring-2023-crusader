using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
                Attack1();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Attack2();
        }
    }

    void Attack1()
    {
        //Play attack animation
        animator.SetTrigger("Attack1");
        //Detect enemies in range of attack

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //Damage them

        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            //enemy.GetComponent<Skeleton_Warrior>().enemyTakeDamage(20);
        }
    }

    void Attack2()
    {
        //Play attack animation
        animator.SetTrigger("Attack2");
        //Detect enemies in range of attack
        //Damage them
        Debug.Log("Right click");
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
