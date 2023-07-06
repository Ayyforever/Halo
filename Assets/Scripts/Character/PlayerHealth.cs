using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public MouseControl mouseControl;
    public DeathPlay deathPlay;
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
        if(hp > 1f)
        {
            hp -= damage;
            damageTime = Time.deltaTime;
            PlayDamageSound();
            if (hp <= 1f)
            {
                Cursor.lockState = CursorLockMode.Confined;
                GameRoot.GetInstance().UIManager_Root.Pop(true);
                GameRoot.GetInstance().UIManager_Root.Push(new DiePanel());
                gameObject.GetComponent<MouseControl>().enabled = false;
                gameObject.GetComponent<MoveControl>().enabled = false;
                Transform childTransform = transform.Find("ÉãÏñ»ú");
                childTransform = childTransform.Find("Weapon");
                childTransform = childTransform.Find("MainWp");
                childTransform.gameObject.SetActive(false);
                deathPlay.PlayDeathSound();
            }
        }
    }
    void PlayDamageSound()
    {
        int randomInt = Random.Range(0, DamageSound.Length);
        audioSource.clip = DamageSound[randomInt];
        audioSource.Play();
    }
}
