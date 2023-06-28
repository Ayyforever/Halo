using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public float warnRange = 50f;
    public float chaseRange = 40f;
    public float attackRange = 8f;

    //攻击角度
    public float attackAngle = 60f;
    //攻击计时
    public float attackTimer = 3f;
    //攻击间隔
    public float timer = 3f;


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
            //警觉动画
            animator.SetBool("warn", true);
            if (distance <= chaseRange && distance > attackRange)
            {
                Chase();
            }
            if (distance <= attackRange)
            {
                if (attackTimer < timer)
                {
                    attackTimer += Time.deltaTime;
                }
                else
                {
                    //协程
                    StartCoroutine(Attack());
                    attackTimer = 0f;
                }
            }
            else
            {
                animator.SetBool("attack", false);
            }

        }
        else
        {
            animator.SetBool("warn", false);
        }
    }

    void Chase()
    {
        agent.isStopped = false;
        animator.SetBool("chase", true);
        agent.SetDestination(player.position);
    }

    private IEnumerator Attack()
    {
        agent.isStopped = true;
        animator.SetTrigger("attackTri");
        yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        //攻击判定
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);
            distance = Vector3.Distance(transform.position, player.position);

            if (angle <= attackAngle && distance <= attackRange)
            {
                //对玩家造成8点伤害
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Damage(8f);
            }
        }
    }
}