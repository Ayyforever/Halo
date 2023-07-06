using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip BGM;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = BGM;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
