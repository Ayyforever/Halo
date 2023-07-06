using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public bool die;
    public float hp = 100f;
    public Animator animator;
    public GameObject CG3;
    public PlayableDirector timeline;

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
            if (audioSource != null)
            {
                audioSource.clip = DeathSound[randomInt];
                audioSource.spatialBlend = 1f;  // ∆Ù”√ 3D “Ù∆µ…Ë÷√
                audioSource.Play();
            }
        }
    }

    void Die()
    {
       
        if(CG3 != null)
        {
            CG3.SetActive(true);
            timeline = CG3.GetComponent<PlayableDirector>();
            timeline.Play();
            timeline.stopped += OnTimelineStopped;
        }
        if (animator == null)
            return;
        animator.SetTrigger("die");
        die = true;
        Destroy(gameObject,3f);
    }
    private void OnTimelineStopped(PlayableDirector director)
    {
        DeactivateCG3();
    }

    private void DeactivateCG3()
    {
        Scene1 scene1 = new Scene1();
        GameRoot.GetInstance().SceneControl_Root.SceneLoad(scene1.SceneName, scene1);
        GameRoot.GetInstance().UIManager_Root.Push(new StartPanel());
        CG3.SetActive(false);
        timeline.stopped -= OnTimelineStopped;
    }
}


