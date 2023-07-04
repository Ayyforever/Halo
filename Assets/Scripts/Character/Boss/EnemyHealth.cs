using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public bool die;
    public float hp = 100f;
    public Animator animator;

    [Header("“Ù∆µ…Ë÷√")]
    public AudioSource audioSource;
    public AudioClip[] DeathSound = new AudioClip[1];

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
            int randomInt = Random.Range(0, DeathSound.Length);
            audioSource.clip = DeathSound[randomInt];
            audioSource.spatialBlend = 1f;  // ∆Ù”√ 3D “Ù∆µ…Ë÷√
            audioSource.Play();
        }
    }

    void Die()
    {
        animator.SetTrigger("die");
        die = true;
        Destroy(gameObject,3f);
    }
}
