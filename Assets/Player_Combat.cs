using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{

    public Animator animator;


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
        //Damage them

    }

    void Attack2()
    {
        //Play attack animation
        animator.SetTrigger("Attack2");
        //Detect enemies in range of attack
        //Damage them
        Debug.Log("Right click");
    }
}
