using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLogic : MonoBehaviour
{
    public NavMeshAgent agent;


    public float attackRange = 2.0f;
    public float attackCooldown = 2f;
    public float attackDamage = 10f;
    public float damageRange = 2f;


    private Transform player;
    private float distance;
    private bool canAttack;


    // Start is called before the first frame update
    void Start()
    {
        
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange)
        {
            canAttack = true;
        }
        if(canAttack)
        {
            Attack();
        }

        


       
    }


    void Attack()
    {
        agent.SetDestination(player.position);
        if(distance <= damageRange)
        {
            //���Ź�������
            Debug.Log("����");
        }

        //����Ƿ񹥻������if

        //���������˺�
        player.gameObject.GetComponent<PlayerHealth>().Damage(attackDamage);
        
    }
}
