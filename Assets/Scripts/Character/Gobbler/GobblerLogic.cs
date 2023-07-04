using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GobblerLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;


    public float chaseRange = 40f;
    public float attackRange = 8f;

    public int endRoll = 3;


    //伤害判断
    public bool damageBool;
    //伤害值
    public float damage = 3f;

    //攻击计时
    private float attackTimer;

    //攻击间隔
    public float timer = 4f;

    public GameObject body;
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
        if (GetComponent<EnemyHealth>().die)
        {
            return;
        }
        Control();
    }




    void Control()
    {

        //获取玩家距离
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            if (attackTimer >= timer)
            {
                Attack();
            }
            else
            {
                attackTimer += Time.deltaTime;
            }
        }
        else
        {
            Chase();
            attackTimer += Time.deltaTime;
        }
        if(endRoll <=0 )
        {
            animator.SetTrigger("endRoll");
        }
    }

    //靠近玩家
    void Chase()
    {
        agent.isStopped = false;
        animator.SetBool("chase", true);
        agent.SetDestination(player.position);
    }


    void Attack()
    {
        agent.isStopped = true;
        //停下行走动画
        animator.SetBool("chase", false);

        endRoll = 3;

        animator.SetTrigger("attack");
        
        attackTimer = 0f;

    }

    void StartRoll()
    {
        if (damageBool)
        {
            player.GetComponent<PlayerHealth>().Damage(damage);
            damageBool = false;
        }
        body.SetActive(true);
        endRoll -= 1;
    }

    void EndRoll()
    {
        body.SetActive(false);
    }

}
