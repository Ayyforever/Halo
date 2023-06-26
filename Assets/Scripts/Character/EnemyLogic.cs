using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLogic : MonoBehaviour
{
    public NavMeshAgent agent;

    //׷������
    public float activeRange = 80.0f;
    //�������
    public float attactRange = 20.0f;
    //��ս����
    public float escapeRange = 120f;

    //�����˺�
    public float damage = 10f;


    public Animator animator;

    private float maxDeviationAngle = 10f; // ���ƫ��Ƕ�


    private Transform player;
    private Transform body;
    private float distance;


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
               Debug.Log("active/////////////////////");
            }
            else
            {
                agent.isStopped = true;
                animator.SetBool("active", false);
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

        // �����������
        Vector3 targetDirection = player.position - body.position;
        Quaternion desiredRotation = Quaternion.LookRotation(targetDirection);

        // ����ƫ��Ƕ�
        float deviationAngle = Random.Range(-maxDeviationAngle, maxDeviationAngle);
        Quaternion deviationRotation = Quaternion.Euler(0f, deviationAngle, 0f);

        // Ӧ��ƫ��Ƕ�
        Quaternion finalRotation = desiredRotation * deviationRotation;
        Vector3 shootingDirection = finalRotation * Vector3.forward;

        // �������߽��й������
        Ray ray = new Ray(transform.position, shootingDirection);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            // �ڹ�����Χ�ڻ�����Ŀ��
            if(hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<PlayerHealth>().Damage(damage);
            }

        }


    }
}
