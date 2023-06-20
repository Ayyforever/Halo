using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private int bulletLeft = 30;
    //一个弹夹子弹
    private int bulletMag = 30;
    //总子弹
    private int bulletTotal = 210;

    public Animator animator;

    
    public TextMeshProUGUI AmmoTextUI;

    private void Start()
    {
        if(animator == null)
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }
    void Update()
    {
        UpdateAmmoUI();
        //重置开火状态
        fire = false;
        //射击
        if (Input.GetMouseButton(0))
        {
            fire = Shoot();
            //播放射击动画
            animator.SetBool("shooting", fire);
        }
        //换弹
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < bulletMag) 
        {
            Reload();
        }
        fireTimer += Time.deltaTime;
    }
     bool Shoot()
    {
        //射击条件判定
        if(fireTimer < fireRate )
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
        //发射射线
         Physics.Raycast(ShootorPoint.position, ShootorPoint.forward, out hit, range);

        if(hit.collider.gameObject.tag == "Enemy")
        {
            hit.collider.gameObject.GetComponent<EnemyHealth>().Damage(20f);
           
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

    //子弹UI
    void UpdateAmmoUI()
    {
        AmmoTextUI.text = bulletLeft + "/" + bulletTotal;
    }
}
