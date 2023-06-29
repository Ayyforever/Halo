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

    public MouseControl mouseControl;

    private Transform player;
    private float distance;
    // ���������Ƶ
    private AudioSource audioSource;
    public AudioClip[] HitSound = new AudioClip[1];
    // ������˺��ӽ�����
    private float damageTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
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
        if(damageTime != 0.0f && damageTime < 0.2f)
        {
            damageTime += Time.deltaTime;
            float yMouse = 2.0f * Mathf.Cos(15.7f * damageTime);
            mouseControl.GetDamage(yMouse);
        }
        if(damageTime >= 0.2f) { damageTime = 0.0f; }
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

            if (angle <= attackAngle && distance <= attackRange)
            {
                //��������8���˺�
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().Damage(8f);
                damageTime = Time.deltaTime;
                PlayHitSound();
            }
        }
    }
    void PlayHitSound()
    {
        int randomInt = Random.Range(0, HitSound.Length);
        audioSource.clip = HitSound[randomInt];
        audioSource.Play();
    }
}