using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] DamageSound = new AudioClip[5];
    public float hp = 100f;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float damage)
    {
        hp -= damage;
        PlayDamageSound();
        if (hp <= 0)
        {
            Debug.Log("die");
            //Die();
        }
    }
    void PlayDamageSound()
    {
        int randomInt = Random.Range(0, DamageSound.Length);
        audioSource.clip = DamageSound[randomInt];
        audioSource.Play();
    }
}
