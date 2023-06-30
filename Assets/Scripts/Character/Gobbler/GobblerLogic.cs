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
    //roar�˺�
  
    //������ʱ
    private float attackTimer;

    //�������
    public float timer = 4f;

    public GameObject collider;
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

        //��ȡ��Ҿ���
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
        }
        if(endRoll <=0 )
        {
            animator.SetTrigger("endRoll");
        }
    }

    //�������
    void Chase()
    {
        agent.isStopped = false;
        animator.SetBool("chase", true);
        agent.SetDestination(player.position);
    }


    void Attack()
    {
        agent.isStopped = true;
        //ͣ�����߶���
        animator.SetBool("chase", false);

        endRoll = 5;

        animator.SetTrigger("attack");
        
        attackTimer = 0f;

    }

    void StartRoll()
    {
        collider.SetActive(true);
    }

    void EndRoll()
    {
        collider.SetActive(false);
        endRoll -= 1;
    }

}
