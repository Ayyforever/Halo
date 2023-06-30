using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public MouseControl mouseControl;
    public AudioSource audioSource;
    public AudioClip[] DamageSound = new AudioClip[5];
    private float damageTime = 0.0f;
    public float hp = 100f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damageTime != 0.0f && damageTime < 0.2f)
        {
            damageTime += Time.deltaTime;
            float yMouse = 2.0f * Mathf.Cos(15.7f * damageTime);
            mouseControl.GetDamage(yMouse);
        }
        if (damageTime >= 0.2f) { damageTime = 0.0f; }
    }
    public void Damage(float damage)
    {
        hp -= damage;
        damageTime = Time.deltaTime;
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
