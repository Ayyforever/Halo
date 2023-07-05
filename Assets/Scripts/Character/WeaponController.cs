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
    [Header("����")]
    public float maxReloadTime;
    private float reloadTime = 0.0f;
    private bool[] reloadSound = new bool[3];
    public MouseControl mouseControl;
    [Header("������")]
    public WeaponSway weaponSway;

    public Animator animator;

    [Header("������Ч")]
    public ParticleSystem particle;

    [Header("�ӵ�Ԥ����")]
    public GameObject bulletPrefab;
    public GameObject bulletFolder;
    public GameObject firePoint;

    [Header("������Ч")]
    public GameObject flashLight;
    public GameObject hitParticle;
    public GameObject hitSmoke;
    public GameObject hitVestige;
    public GameObject hitBlood;

    [Header("��Ƶ����")]
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
        //����
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
            //���
            else if (Input.GetMouseButton(0))
            {
                fire = Shoot();
            }
            fireTimer += Time.deltaTime;
        }
        
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
            return false;
        }
        else if(bulletLeft == 0)
        {
            PlayNoBulletSound();
            fireTimer = -0.2f;
            return false;
        }
        
        //���е�
        RaycastHit hit;
        // ������
        weaponSway.Recoil();
        // ������Ч
        PlayShootSound(bulletLeft);

        //����Ч��
        particle.Play();
        flashLight.SetActive(true);
        // �����ӵ�
        GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation, bulletFolder.transform);
        //��������
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
