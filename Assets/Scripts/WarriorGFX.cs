using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WarriorGFX : MonoBehaviour
{
    public float attackDistance;
    public float timer;
    private GameObject target;
    private bool inRange;
    private bool cooling;
    private float intTimer;
    private bool attackMode;
    public bool faceLeft = true;

    private RaycastHit2D hit;
    public Transform rayCast;
    public LayerMask raycastMask;
    public float rayCastLength;

    public float moveSpeed;
    private float distance;

    private Animator anim;
    // Update is called once per frame

    private void Awake()
    {
        intTimer = timer;
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        /* if(aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        } */
        if(inRange)
        {
            hit = Physics2D.Raycast(rayCast.position, Vector2.left, rayCastLength, raycastMask);
            RaycastDebugger();
        }

        if(hit.collider != null){
            EnemyLogic();
        } else if (hit.collider==null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            anim.SetBool("canWalk", false);
            StopAttack();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.transform.position);

        if(distance > attackDistance)
        {
            Move();
        }
        else if(attackDistance >= distance && cooling == false)
        {
            anim.SetBool("canAttack1", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("SkeletonWarriorcanAttack1"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            float direction = transform.position.x - targetPosition.x;
            if( direction < 0 && faceLeft == true)
            {
                faceLeft = false;
                Vector3 theScale = transform.localScale;
		        theScale.x *= -1;
		        transform.localScale = theScale;
            }
            if( direction >= 0 && faceLeft == false)
            {
                faceLeft = true;
                Vector3 theScale = transform.localScale;
		        theScale.x *= -1;
		        transform.localScale = theScale;
            }
        }
    }

    void Attack()
    {
        Debug.Log("Attack");
        timer = intTimer;
        attackMode = true;

        anim.SetBool("canWalk", false);
        anim.SetBool("canAttack1", true);
    }

    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("canAttack1", false);
    }

    void OnTriggerEnter2D(Collider2D trig){
        if(trig.gameObject.tag == "Player")
        {
            target = trig.gameObject;
            inRange=true;
        }
    }

    void RaycastDebugger()
    {
        if(distance > attackDistance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.red);
        }
        else if (attackDistance > distance)
        {
            Debug.DrawRay(rayCast.position, Vector2.left * rayCastLength, Color.green);
        }
    }
}
