using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundControl : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] ExplosionSound = new AudioClip[4];
    // Start is called before the first frame update
    void Start()
    {
        int randomInt = Random.Range(0, ExplosionSound.Length);
        audioSource.clip = ExplosionSound[randomInt];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
