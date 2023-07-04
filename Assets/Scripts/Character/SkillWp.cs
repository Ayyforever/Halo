using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWp : MonoBehaviour
{
    public Animator animator;

    //能量
    public float power = 0f;
    private bool grenadeMode;

    public float fireTimer = 0.0f;

    public GameObject firePoint;
    public GameObject grenadePrefab;
    public GameObject grenadeFolder;

    public GameObject GrenadeMode;

    public AudioSource audioSource;
    public AudioClip[] EmitSound = new AudioClip[4];
    public AudioClip[] ActivateSound = new AudioClip[1];

    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Grenade();
        if (grenadeMode)
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();
            }
            if(fireTimer >= 1f)
            {
                GrenadeMode.SetActive(true);
            }
            timer += Time.deltaTime;
            fireTimer += Time.deltaTime;
        }
        if (timer >= 20f && grenadeMode == true)
        {
            grenadeMode = false;
            //激活正常射击脚本
            gameObject.GetComponent<WeaponController>().enabled = true;
            GrenadeMode.SetActive(false);
            //重置计时器
            timer = 0;
            fireTimer = 0.0f;
        }
    }


    void Grenade()
    {
        if (Input.GetKeyDown(KeyCode.Q) && power >= 0f)
        {
            //关闭正常射击脚本
            gameObject.GetComponent<WeaponController>().enabled = false;
            grenadeMode = true;
            int randomInt = Random.Range(0, ActivateSound.Length);
            audioSource.clip = ActivateSound[randomInt];
            audioSource.Play();
        }
    }

    void Shoot()
    {
        if(fireTimer < 1f)
        {
            return;
        }
        int randomInt = Random.Range(0, EmitSound.Length);
        audioSource.clip = EmitSound[randomInt];
        audioSource.Play();
        animator.SetTrigger("fight");
        //发射子弹
        // 实例化榴弹预制体
        GameObject grenade = Instantiate(grenadePrefab, firePoint.transform.position,firePoint.transform.rotation, grenadeFolder.transform);

        //// 获取榴弹刚体组件
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        //// 应用发射力度
        rb.AddForce(firePoint.transform.up * 25f, ForceMode.Impulse);

        Destroy(grenade, 5.0f);
        GrenadeMode.SetActive(false);
        fireTimer = 0f;
    }

}
