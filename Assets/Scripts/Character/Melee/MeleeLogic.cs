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

    public GameObject claw;

    public bool damageBool;
    public float damage = 8f;
    //π•ª˜º∆ ±
    public float attackTimer;
    //π•ª˜º‰∏Ù
    public float timer = 3f;

    private Transform player;
    private float distance;
    [Header("“Ù∆µ…Ë÷√")]
    public AudioSource[] audioSource = new AudioSource[2];
    [Header("ª˜¥ÚÕÊº“")]
    public AudioClip[] HitSound = new AudioClip[1];
    [Header("π•ª˜…˘")]
    public AudioClip[] AttackSound = new AudioClip[2];

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
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= warnRange)
        {
            //æØæı∂Øª≠
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
    
       
        attackTimer = 0f;
    }

    void ClawActive()
    {

        claw.SetActive(true); 
    }

    void ClawDown()
    {
        //…À∫¶
        if (damageBool)
        {
            player.GetComponent<PlayerHealth>().Damage(damage);
            damageBool = false;
            PlayHitSound();
        }
        claw.SetActive(false);
    }

    void PlayAttackSound()
    {
        int randomInt = Random.Range(0, AttackSound.Length);
        audioSource[1].clip = AttackSound[randomInt];
        audioSource[1].spatialBlend = 1f;  // ∆Ù”√ 3D “Ù∆µ…Ë÷√
        audioSource[1].Play();
    }
    void PlayHitSound()
    {
        int randomInt = Random.Range(0, HitSound.Length);
        audioSource[0].clip = HitSound[randomInt];
        audioSource[0].Play();
    }
}