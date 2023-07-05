using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update

    //开火点
    public Transform ShootorPoint;
    //射击范围
    public float range = 100f;
    //射击速度
    private float fireRate = 0.1f;
    //射击计时
    private float fireTimer = 0;
    //是否真正射击
    private bool fire;
    //当前子弹数
    public int bulletLeft = 30;
    //一个弹夹子弹
    public int bulletMag = 30;
    //总子弹
    public int bulletTotal = 210;
    [Header("换弹")]
    public float maxReloadTime;
    private float reloadTime = 0.0f;
    private bool[] reloadSound = new bool[3];
    public MouseControl mouseControl;
    [Header("后座力")]
    public WeaponSway weaponSway;

    public Animator animator;

    [Header("开火特效")]
    public ParticleSystem particle;

    [Header("子弹预制体")]
    public GameObject bulletPrefab;
    public GameObject bulletFolder;
    public GameObject firePoint;

    [Header("击中特效")]
    public GameObject flashLight;
    public GameObject hitParticle;
    public GameObject hitSmoke;
    public GameObject hitVestige;
    public GameObject hitBlood;

    [Header("音频设置")]
    public AudioSource[] audioSource = new AudioSource[2];
    public AudioClip[] WeaponSound = new AudioClip[5];
    public AudioClip[] ReloadSound = new AudioClip[3];
    public AudioClip[] NoBulletSound = new AudioClip[3];

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        for (int i = 0; i < reloadSound.Length; i++) { reloadSound[i] = true; }
    }
    void Update()
    {
        //换弹
        if (!InventorySystem.Instance.isOpen)
        {
            bool A = Input.GetKeyDown(KeyCode.R) && bulletLeft < bulletMag;
            bool B = bulletLeft == 0;
            if ((A || B) && bulletTotal != 0 && reloadTime == 0.0f)
            {
                animator.SetTrigger("reload");
                reloadTime = Time.deltaTime;
            }
            if (reloadTime != 0.0f)
            {
                Reload();
            }
            //射击
            else if (Input.GetMouseButton(0))
            {
                fire = Shoot();
            }
            fireTimer += Time.deltaTime;
        }
        
    }
     bool Shoot()
    {
        //射击条件判定
        //检测是否在换弹
        //if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        //{
        //    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        //    {
        //        return false;
        //    }
        //}
        if (fireTimer < fireRate )
        {
            return false;
        }
        if(bulletLeft <= 0 && bulletTotal != 0)
        {
            return false;
        }
        else if(bulletLeft == 0)
        {
            PlayNoBulletSound();
            fireTimer = -0.2f;
            return false;
        }
        
        //击中点
        RaycastHit hit;
        // 后座力
        weaponSway.Recoil();
        // 播放音效
        PlayShootSound(bulletLeft);

        //开火效果
        particle.Play();
        flashLight.SetActive(true);
        // 发射子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation, bulletFolder.transform);
        //发射射线
        if (Physics.Raycast(ShootorPoint.position, ShootorPoint.forward, out hit, range))
        {
            GameObject hitParticleOb = Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            GameObject hitSmokeOb = Instantiate(hitSmoke, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            Destroy(hitParticleOb, 0.2f);
            Destroy(hitSmokeOb, 1.0f);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                GetComponent<SkillWp>().power++;
                hit.collider.gameObject.GetComponentInParent<EnemyHealth>().Damage(20f);
                GameObject hitBloodOb = Instantiate(hitBlood, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                Destroy(hitBloodOb, 0.5f);
            }
            else
            {
                GameObject hitVestigeOb = Instantiate(hitVestige, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
                Destroy(hitVestigeOb, 5.0f);
            }
        }

        fireTimer = 0;
        bulletLeft--;
        return true;
    }
    //换弹
    void Reload()
    {
        reloadTime += Time.deltaTime;
        if(reloadTime >= maxReloadTime / 6 && reloadTime < maxReloadTime / 2 && reloadSound[0])
        {
            PlayReloadSound(0);
            reloadSound[0] = false;
        }
        else if (reloadTime >= maxReloadTime / 4 && reloadTime < 3 * maxReloadTime / 8)
        {
            float yMouse = 0.5f * Mathf.Cos(25.12f * (reloadTime - maxReloadTime / 4) / maxReloadTime);
            mouseControl.Reload_MouseControl(yMouse);
        }
        else if(reloadTime >= maxReloadTime / 2 && reloadTime < 17 * maxReloadTime / 24 && reloadSound[1])
        {
            PlayReloadSound(1);
            reloadSound[1] = false;
        }
        else if (reloadTime >= 7 * maxReloadTime / 12 && reloadTime < 17 * maxReloadTime / 24)
        {
            float yMouse = -0.3f * Mathf.Cos(25.12f * (reloadTime - 7 * maxReloadTime / 12) / maxReloadTime);
            mouseControl.Reload_MouseControl(yMouse);
        }
        else if (reloadTime >= 17 * maxReloadTime / 24 && reloadTime < maxReloadTime && reloadSound[2])
        {
            PlayReloadSound(2);
            reloadSound[2] = false;
        }
        else if (reloadTime >= 18 * maxReloadTime / 24 && reloadTime < maxReloadTime)
        {
            float yMouse = -0.3f * Mathf.Cos(15.7f * (reloadTime - 17 * maxReloadTime / 24) / maxReloadTime);
            mouseControl.Reload_MouseControl(yMouse);
        }
        else if(reloadTime >= maxReloadTime)
        {
            if (bulletTotal >= bulletMag - bulletLeft)
            {
                bulletTotal -= (bulletMag - bulletLeft);
                bulletLeft = bulletMag;
            }
            else
            {
                bulletLeft += bulletTotal;
                bulletTotal = 0;
            }
            reloadTime = 0.0f;
            for(int i = 0; i < reloadSound.Length; i++) { reloadSound[i] = true; }
        }
    }
    void PlayNoBulletSound()
    {
        int randomInt = Random.Range(0, NoBulletSound.Length);
        audioSource[0].clip = NoBulletSound[randomInt];
        audioSource[0].Play();
    }
    void PlayShootSound(int bullet)
    {
        int randomInt = Random.Range(0, WeaponSound.Length);
        audioSource[bullet % 2].clip = WeaponSound[randomInt];
        audioSource[bullet % 2].Play();
    }
    void PlayReloadSound(int n)
    {
        audioSource[0].clip = ReloadSound[n];
        audioSource[0].Play();
    }
}
