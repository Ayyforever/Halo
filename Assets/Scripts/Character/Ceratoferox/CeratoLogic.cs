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


    //�˺��ж�
    public bool damageBool;
    //�˺�ֵ
    public float damage = 6f;

    //roar�������
    public int n = 3;
    //roar�˺�
    public float roarDamage = 4f;
    //������ʱ
    private float attackTimer;

    //�������
    public float timer = 4f;

    public GameObject claw1;
    //public GameObject claw2;
    private Transform player;
    private float distance;
    [Header("��Ƶ����")]
    public AudioSource audioSource;
    [Header("����")]
    public AudioClip[] AttackSound = new AudioClip[2];
    [Header("����")]
    public AudioClip[] RoarSound = new AudioClip[2];

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
        int randomInt = Random.Range(0, AttackSound.Length);
        audioSource.clip = AttackSound[randomInt];
        audioSource.Play();
        attackTimer = 0f;
        
    }


    void Roar()
    {
        agent.isStopped = true;
        animator.SetBool("chase", false);
        animator.SetTrigger("roar");
        int randomInt = Random.Range(0, RoarSound.Length);
        audioSource.clip = RoarSound[randomInt];
        audioSource.Play();
        //1.5��������Χֱ�ӿ�Ѫ
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange * 1.5f);
        foreach (Collider obj in colliders)
        {
            if (obj.gameObject.CompareTag("Player"))
            {
                player.GetComponent<PlayerHealth>().Damage(roarDamage);
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
        if (damageBool)
        {
            player.GetComponent<PlayerHealth>().Damage(damage);
            damageBool = false;
        }
        claw1.SetActive(false);
        n--;
    }
}
