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
    private float fireRate = 0.25f;
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
        
        //换弹
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < bulletMag) 
        {
            Reload();
        }
        //射击
        if (Input.GetMouseButton(0))
        {
            fire = Shoot();
            
        }
        
        fireTimer += Time.deltaTime;
    }
     bool Shoot()
    {
        //射击条件判定
        //检测是否在换弹
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
        //击中点
        RaycastHit hit;
        //播放动画
        animator.CrossFadeInFixedTime("Shooting", 0.1f);

        //开火效果
        particle.Play();
        flashLight.SetActive(true);
        //发射射线
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

    //换弹
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
