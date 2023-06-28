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
    private float fireRate = 0.25f;
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

    public Animator animator;

    public ParticleSystem particle;

    public GameObject hitEffect;
    public GameObject flashLight;
    public GameObject hitParticle;
    public GameObject hitSmoke;
    public GameObject hitVestige;

    private void Start()
    {
           animator = gameObject.GetComponent<Animator>();
        hitEffect.SetActive(false);
    }
    void Update()
    {
        
        //����
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < bulletMag) 
        {
            Reload();
        }
        //���
        if (Input.GetMouseButton(0))
        {
            fire = Shoot();
            
        }
        
        fireTimer += Time.deltaTime;
    }
     bool Shoot()
    {
        //��������ж�
        //����Ƿ��ڻ���
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                return false;
            }
        }
        if (fireTimer < fireRate )
        {
            return false;
        }
        if(bulletLeft <= 0)
        {
            Reload();
            return false;
        }
        //���е�
        RaycastHit hit;
        //���Ŷ���
        animator.CrossFadeInFixedTime("Shooting", 0.1f);

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
        animator.SetTrigger("reload");
        if(bulletTotal >= bulletMag - bulletLeft)
        {
            bulletTotal -= (bulletMag - bulletLeft);
            bulletLeft = bulletMag;
        }
        else
        {
            bulletTotal = 0;
            bulletLeft += bulletTotal;
        }
    }
}
