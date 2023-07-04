using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSoundControl : MonoBehaviour
{
    public MouseControl mouseControl;
    public AudioSource audioSource;
    public AudioClip[] ExplosionSound = new AudioClip[4];
    private float explosionTimer = 0.0f;
    private float maxExplosionTimer = 1.8f;
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
        if(explosionTimer < 0.5f * maxExplosionTimer)
        {
            explosionTimer += Time.deltaTime;
            Vector3 playerPosition = mouseControl.Player_Object.position;
            float d = 10 - Vector3.Distance(playerPosition, transform.position) / 5;
            d *= 0.05f;
            float xMove = d * Mathf.Sin(100.48f / maxExplosionTimer * explosionTimer) * (maxExplosionTimer / 2 - explosionTimer);
            float yMove = d * Mathf.Cos(100.48f / maxExplosionTimer * explosionTimer) * (maxExplosionTimer / 2 - explosionTimer);
            mouseControl.Explosion_MouseControl(xMove, yMove);
        }
    }
}
