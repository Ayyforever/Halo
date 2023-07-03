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

    //�����Ƕ�
    public float attackAngle = 60f;
    //������ʱ
    public float attackTimer = 3f;
    //�������
    public float timer = 3f;

    private Transform player;
    private float distance;
    [Header("��Ƶ����")]
    public AudioSource[] audioSource = new AudioSource[2];
    [Header("�������")]
    public AudioClip[] HitSound = new AudioClip[1];
    [Header("������")]
    public AudioClip[] AttackSound = new AudioClip[2];

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
        yield return new WaitForSeconds(0.6f/*animator.GetCurrentAnimatorClipInfo(0)[0].clip.length*/);
        //�����ж�
        {
            Vector3 direction = (player.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);
            distance = Vector3.Distance(transform.position, player.position);
            PlayAttackSound();
            if (angle <= attackAngle && distance <= attackRange)
            {
                //��������8���˺�
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Damage(8f);
                PlayHitSound();
            }
        }
    }
    void PlayAttackSound()
    {
        int randomInt = Random.Range(0, AttackSound.Length);
        audioSource[1].clip = AttackSound[randomInt];
        audioSource[1].spatialBlend = 1f;  // ���� 3D ��Ƶ����
        audioSource[1].Play();
    }
    void PlayHitSound()
    {
        int randomInt = Random.Range(0, HitSound.Length);
        audioSource[0].clip = HitSound[randomInt];
        audioSource[0].Play();
    }
}