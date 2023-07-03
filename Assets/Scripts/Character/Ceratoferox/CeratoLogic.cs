using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.AI;

public class CeratoLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;


    public float chaseRange = 40f;
    public float attackRange = 8f;

    //roar¹¥»÷¼ä¸ô
    public int n = 3;
    //roarÉËº¦
    public float roarDamage = 3f;
    //¹¥»÷¼ÆÊ±
    private float attackTimer;

    //¹¥»÷¼ä¸ô
    public float timer = 4f;

    public GameObject claw1;
    //public GameObject claw2;
    private Transform player;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        attackTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }




    void Control()
    {

        //»ñÈ¡Íæ¼Ò¾àÀë
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            if (attackTimer >= timer)
            {
                if (n >= 0)
                {
                    Attack();

                }
                else
                {
                    //roar¹¥»÷·¶Î§µôÑª
                    Roar();
                }
            }
            else
            {
                attackTimer += Time.deltaTime;
            }
        }
        else
        {
            Chase();
        }
    }

    //¿¿½üÍæ¼Ò
    void Chase()
    {
        agent.isStopped = false;
        animator.SetBool("chase", true);
        agent.SetDestination(player.position);
    }


    void Attack()
    {
        agent.isStopped = true;
        //Í£ÏÂÐÐ×ß¶¯»­
        animator.SetBool("chase", false);
        
        //²¥·Å¹¥»÷¶¯»­
        animator.SetTrigger("attack");

        attackTimer = 0f;
        
    }


    void Roar()
    {
        agent.isStopped = true;
        animator.SetBool("chase", false);
        animator.SetTrigger("roar");
        //·¶Î§¹¥»÷¿ÛÑª
        Collider[] colliders = Physics.OverlapSphere(transform.position, 8f);
        foreach(Collider collider in colliders)
        {
            if (collider.GetComponent<PlayerHealth>() != null)
            {
                collider.GetComponent<PlayerHealth>().Damage(roarDamage);
            }
        }
        n = 2;

        attackTimer = 0f;
    }
    void ClawActive()
    {
        claw1.SetActive(true);
    }

    void ClawDown()
    {
        claw1.SetActive(false);
        n--;
    }
}
