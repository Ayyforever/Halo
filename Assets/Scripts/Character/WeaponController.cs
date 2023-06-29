using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class WeaponController : MonoBehaviour
{
    // Start is called before the first frame update

    //�����
    public Transform ShootorPoint;
    //�����Χ
    public float range = 100f;
    //����ٶ�
    private float fireRate = 0.1f;
    //�����ʱ
    private float fireTimer = 0;
    //�Ƿ��������
    private bool fire;
    //��ǰ�ӵ���
    public int bulletLeft = 30;
    //һ�������ӵ�
    public int bulletMag = 30;
    //���ӵ�
    public int bulletTotal = 210;
    // ����
    public float maxReloadTime;
    private float reloadTime = 0.0f;
    private bool[] reloadSound = new bool[3];
    public MouseControl mouseControl;
    //������
    public WeaponSway weaponSway;

    public Animator animator;

    // ������Ч
    public ParticleSystem particle;

    // ������Ч
    public GameObject hitEffect;
    public GameObject flashLight;
    public GameObject hitParticle;
    public GameObject hitSmoke;
    public GameObject hitVestige;

    // ��Ч
    private AudioSource audioSource;
    public AudioClip[] WeaponSound = new AudioClip[5];
    public AudioClip[] ReloadSound = new AudioClip[3];
    public AudioClip[] NoBulletSound = new AudioClip[3];

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        hitEffect.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        for (int i = 0; i < reloadSound.Length; i++) { reloadSound[i] = true; }
    }
    void Update()
    {
        //����
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < bulletMag && bulletTotal != 0) 
        {
            animator.SetTrigger("reload");
            reloadTime = Time.deltaTime;
        }
        if(reloadTime != 0.0f)
        {
            Reload();
        }
        //���
        else if(Input.GetMouseButton(0))
        {
            fire = Shoot();
        }
        fireTimer += Time.deltaTime;
    }
     bool Shoot()
    {
        //��������ж�
        //����Ƿ��ڻ���
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
            animator.SetTrigger("reload");
            reloadTime = Time.deltaTime;
            return false;
        }
        else if(bulletLeft <= 0)
        {
            PlayNoBulletSound();
            fireTimer = 0.0f;
            return false;
        }
        
        //���е�
        RaycastHit hit;
        // ������
        weaponSway.Recoil();
        // ������Ч
        PlayShootSound();

        //����Ч��
        particle.Play();
        flashLight.SetActive(true);
        //��������
        if (Physics.Raycast(ShootorPoint.position, ShootorPoint.forward, out hit, range))
        {
            hitEffect.SetActive(true);
            GameObject hitParticleOb = Instantiate(hitParticle, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            GameObject hitSmokeOb = Instantiate(hitSmoke, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            GameObject hitVestigeOb = Instantiate(hitVestige, hit.point, Quaternion.FromToRotation(Vector3.forward, hit.normal));
            hitEffect.SetActive(false);
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponentInParent<EnemyHealth>().Damage(20f);
            }
            Destroy(hitVestigeOb, 5.0f);
            Destroy(hitSmokeOb, 1.0f);
            Destroy(hitParticleOb, 0.2f);
        }

        fireTimer = 0;
        bulletLeft--;
        return true;
    }
    //����
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
        audioSource.clip = NoBulletSound[randomInt];
        audioSource.Play();
    }
    void PlayShootSound()
    {
        int randomInt = Random.Range(0, WeaponSound.Length);
        audioSource.clip = WeaponSound[randomInt];
        audioSource.Play();
    }
    void PlayReloadSound(int n)
    {
        audioSource.clip = ReloadSound[n];
        audioSource.Play();
    }
}
