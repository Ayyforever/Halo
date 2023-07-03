using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeLogic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public float warnRange = 30f;
    public float chaseRange = 20f;
    public float attackRange = 5f;

    //�����Ƕ�
    public float attackAngle = 60f;
    //������ʱ
    public float attackTimer = 3f;
    //�������
    public float timer = 3f;

    public GameObject collider;

    private Transform player;
    private float distance;
    [Header("��Ƶ����")]
    public AudioSource[] audioSource = new AudioSource[2];
    [Header("�������")]
    public AudioClip[] HitSound = new AudioClip[1];
    [Header("�������")]
    public AudioClip[] ScreamSound = new AudioClip[5];
    private float screamTime = 0.0f;
    private float maxScreamTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        maxScreamTime = Random.Range(10f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        screamTime += Time.deltaTime;
        if (screamTime >= maxScreamTime)
        {
            PlayScreamSound();
            screamTime = 0.0f;
            maxScreamTime = Random.Range(10f, 20f);
        }
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= warnRange)
        {
            //��������
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
                    //Э��
                    Attack();
                   
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

    void Attack()
    {
        agent.isStopped = true;
        animator.SetTrigger("attackTri");
        /*
        //�����ж�             // ������ײ��������ʶ���
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);
            distance = Vector3.Distance(transform.position, player.position);

            if (angle <= attackAngle && distance <= attackRange)
            {
                //��������8���˺�
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Damage(8f);
                PlayHitSound();
            }
        }*/
        attackTimer = 0f;
    }
    void PlayScreamSound()
    {
        int randomInt = Random.Range(0, ScreamSound.Length);
        audioSource[1].clip = ScreamSound[randomInt];
        audioSource[1].spatialBlend = 1f;  // ���� 3D ��Ƶ����
        audioSource[1].Play();
    }
    void PlayHitSound()
    {
        int randomInt = Random.Range(0, HitSound.Length);
        audioSource[0].clip = HitSound[randomInt];
        audioSource[0].Play();
    }

    //������ײ�弤��
    void Active()
    {
        collider.SetActive(true);
    }

    void InActive()
    {
        collider.SetActive(false);
    }
}