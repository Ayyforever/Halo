using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Logic : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    //ÉËº¦ÅÐ¶Ï
    public bool damageBool;
    //ÉËº¦Öµ
    public float damage=3f;


    public float chaseRange = 40f;
    public float attackRange = 8f;


    //¹¥»÷¼ÆÊ±
    private float attackTimer;

    //¹¥»÷¼ä¸ô
    public float timer = 2f;

    public GameObject claw1;
    public GameObject claw2;
    private Transform player;
    private float distance;
    [Header("ÒôÆµÉèÖÃ")]
    public AudioSource audioSource;
    [Header("¹¥»÷")]
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

        //»ñÈ¡Íæ¼Ò¾àÀë
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

    //¿¿½üÍæ¼Ò
    void Chase()
    {
        agent.isStopped = false;
        animator.SetBool("chase", true);
        agent.SetDestination(player.position);
    }


    void Attack()
    {
        agent.isStopped = true;
        //Í£ÏÂÐÐ×ß¶¯»­
        animator.SetBool("chase", false);
        //²¥·Å¹¥»÷¶¯»­
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
        //ÉËº¦
        if (damageBool)
        {
            player.GetComponent<PlayerHealth>().Damage(damage);
            damageBool = false;
        }
        claw1.SetActive(false);
        claw2.SetActive(false);
    }
}