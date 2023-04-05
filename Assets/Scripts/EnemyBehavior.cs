using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    public UnitHealth enemyHealth = new UnitHealth(120,120);
    public Transform attackPoint;
    public LayerMask playerLayers;
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;
    public float attackDistance;
    public float moveSpeed;
    public float timer;
    public Transform leftLimit;
    public Transform rightLimit;
public float attackRange = 0.5f;
    private RaycastHit2D hit;
    private Transform target;
    private Animator anim;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool cooling;
    private float intTimer;

     void Awake()
    {
        SelectTarget();
        intTimer = timer;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if(!attackMode)
        {
            Move();
        }
        if(!InsideOfLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("SkeletonWarriorcanAttack1"))
        {
            SelectTarget();
        }
        if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, transform.right, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        if(hit.collider != null){
            EnemyLogic();
        } 
        else if (hit.collider==null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            StopAttack();
        }
    }

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackDistance)
        {
            StopAttack();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            Attack();
        }

        if(cooling)
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("SkeletonWarriorcanAttack1"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer;
        attackMode = true;
        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(attackPoint.position, attackPoint.localScale, playerLayers);
        foreach(Collider2D player in hitPlayer)
        {
            Debug.Log("Enemy hit " + player.name);
            //enemy.GetComponent<Skeleton_Warrior>().enemyTakeDamage(20);
        }
        anim.SetBool("canWalk", false);
        anim.SetBool("canAttack1", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("canAttack1", false);
    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, transform.right * rayCastLength, Color.green);
        }
    }

    public void TriggerCooling(){
        cooling=true;
    }

    private bool InsideOfLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightLimit.position.x;
    }

    private void SelectTarget()
    {
        float distanceToLeft = Vector3.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector3.Distance(transform.position, rightLimit.position);

        if(distanceToLeft > distanceToRight)
        {
            target = leftLimit;
        }
        else
        {
            target = rightLimit;
        }

        Flip();
    }

    private void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if(transform.position.x > target.position.x) {
            rotation.y = 0f;
        }
        else
        {
            rotation.y = 180f;
        }
        
        transform.eulerAngles = rotation;
    }

    private void enemyTakeDamage(int dmg)
    {
        enemyHealth.DamageUnit(20);

        if(enemyHealth.Health <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log("Enemy Died");
    }


}
