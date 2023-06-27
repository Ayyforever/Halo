using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public float warnRange = 25f;
    public float chaseRange = 20f;
    public float attackRange = 1f;


    private Transform player;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= warnRange)
        {
            //¾¯¾õ¶¯»­
            animator.SetBool("warn", true);
            if (distance <= chaseRange)
            {
                Chase();
                if(distance <= attackRange)
                {
                    Attack();
                }
                else
                {
                    animator.SetBool("attack", false);
                }
            }
        }
        else
        {
            animator.SetBool("warn", false);
        }
    }

    void Chase()
    {
        animator.SetTrigger("chase");
        agent.SetDestination(player.position);
    }

    void Attack()
    {
        animator.SetBool("attack", true);
    }
}
