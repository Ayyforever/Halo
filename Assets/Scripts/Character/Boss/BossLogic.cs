using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BossLogic : MonoBehaviour
{
    public NavMeshAgent agent;

    //×·»÷¾àÀë
    public float activeRange = 80.0f;
    //Éä»÷¾àÀë
    public float attactRange = 20.0f;
    //ÍÑÕ½¾àÀë
    public float escapeRange = 120f;


    //¹¥»÷ÆµÂÊ
    public float attackTimer = 3f;
    //¹¥»÷ÉËº¦
    public float damage = 10f;


    public Animator animator;

    private float maxDeviationAngle = 0f; // ×î´óÆ«Àë½Ç¶È

    private float n = 0;



    private Transform player;
    private Transform body;
    private float distance;

    public LineRenderer SmallCanon01L;
    public LineRenderer SmallCanon01R;
    public LineRenderer SmallCanon02L;
    public LineRenderer SmallCanon02R;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        body = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(body.position, player.position);
        if (distance <= activeRange)
        {
            if (distance > attactRange)
            {
               Active();
               
            }
            else
            {
                agent.isStopped = true;
                animator.SetBool("active", false);
                attackTimer += Time.deltaTime;
                Attack();
            }
        }
        else if(distance > escapeRange)
        {
            agent.isStopped = true;
        }


        
    }

    void Active()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);
        animator.SetBool("active", true);

    }
    void Attack()
    {
        if(attackTimer < 3f)
        {
            return;
        }
        
        gameObject.transform.LookAt(player.position);
        // ¼ÆËãÉä»÷·½Ïò
        Vector3 targetDirection = player.position - body.position;
        Quaternion desiredRotation = Quaternion.LookRotation(targetDirection);

       // ¼ÆËãÆ«Àë½Ç¶È
       // float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle);
       // Quaternion deviationRotation = Quaternion.Euler(0f, deviationAngle, 0f);

       // Ó¦ÓÃÆ«Àë½Ç¶È
       //Quaternion finalRotation = desiredRotation * deviationRotation;
       // Vector3 shootingDirection = finalRotation * Vector3.forward;

        // ·¢ÉäÉäÏß½øÐÐ¹¥»÷¼ì²â
        Ray ray = new Ray(transform.position, targetDirection);
        RaycastHit hit;
        

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // ÔÚ¹¥»÷·¶Î§ÄÚ»÷ÖÐÁËÄ¿±ê
            if(hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerHealth>().Damage(damage);
                Debug.Log("n"+n++);
            }

        }

        attackTimer = 0f;

    }
}
