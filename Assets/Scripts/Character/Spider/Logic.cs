using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Logic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;


    public float chaseRange = 40f;
    public float attackRange = 8f;


    //攻击计时
    private float attackTimer;

    //攻击间隔
    public float timer = 2f;

    public GameObject claw1;
    public GameObject claw2;
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

        //获取玩家距离
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            if (attackTimer >= timer)
            {
                StartCoroutine(Attack());
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

    //靠近玩家
    void Chase()
    {
        agent.isStopped = false;
        animator.SetBool("chase", true);
        agent.SetDestination(player.position);
    }


    IEnumerator Attack()
    {
        agent.isStopped = true;
        //停下行走动画
        animator.SetBool("chase", false);
        ClawActive();
        //播放攻击动画
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        ClawDown();

        attackTimer = 0f;
    }

    void ClawActive()
    {
        claw1.SetActive(true);
        claw2.SetActive(true);
    }

    void ClawDown()
    {
        claw1.SetActive(false);
        claw2.SetActive(false);
    }
}