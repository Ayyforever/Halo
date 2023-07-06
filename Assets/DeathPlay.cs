using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlay : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip DeathSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayDeathSound()
    {
        audioSource.volume = 1.0f;
        audioSource.clip = DeathSound;
        audioSource.Play();
    }
}
