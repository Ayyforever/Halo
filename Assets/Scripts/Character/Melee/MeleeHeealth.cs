using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHeealth : MonoBehaviour
{
    public float hp = 100f;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Damage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("die");
        Destroy(gameObject, 3f);
    }
}
