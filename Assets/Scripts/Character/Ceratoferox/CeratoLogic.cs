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

    //roar�������
    public int n = 3;
    //roar�˺�
    public float roarDamage = 3f;
    //������ʱ
    private float attackTimer;

    //�������
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

        //��ȡ��Ҿ���
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            if (attackTimer >= timer)
            {
                if (n > 0)
                {
                    Attack();

                }
                else
                {
                    //roar�����ٷְٵ�Ѫ
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
        
        //���Ź�������
        animator.SetTrigger("attack");

        attackTimer = 0f;
        
    }


    void Roar()
    {
        agent.isStopped = true;
        animator.SetBool("chase", false);
        animator.SetTrigger("roar");
        //1.5��������Χֱ�ӿ�Ѫ
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange * 1.5f);
        foreach (Collider obj in colliders)
        {
            if (obj.gameObject.CompareTag("Player"))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Damage(roarDamage);
            }
        }
        n = 3;

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
