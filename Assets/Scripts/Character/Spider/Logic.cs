using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Logic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    //�˺��ж�
    public bool damageBool;
    //�˺�ֵ
    public float damage=3f;


    public float chaseRange = 40f;
    public float attackRange = 8f;


    //������ʱ
    private float attackTimer;

    //�������
    public float timer = 2f;

    public GameObject claw1;
    public GameObject claw2;
    private Transform player;
    private float distance;
    [Header("��Ƶ����")]
    public AudioSource audioSource;
    [Header("����")]
    public AudioClip[] AttackSound = new AudioClip[2];

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
        if(GetComponent<EnemyHealth>().die)
        {
            return;
        }
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
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        int randomInt = Random.Range(0, AttackSound.Length);
        audioSource.clip = AttackSound[randomInt];
        audioSource.Play();
        attackTimer = 0f;
    }

    void ClawActive()
    {
        
        claw1.SetActive(true);
        claw2.SetActive(true);
    }

    void ClawDown()
    {
        //�˺�
        if (damageBool)
        {
            player.GetComponent<PlayerHealth>().Damage(damage);
            damageBool = false;
        }
        claw1.SetActive(false);
        claw2.SetActive(false);
    }
}