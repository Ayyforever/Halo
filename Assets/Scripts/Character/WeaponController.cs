using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    private int bulletLeft = 30;
    //һ�������ӵ�
    private int bulletMag = 30;
    //���ӵ�
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
        //���ÿ���״̬
        fire = false;
        //���
        if (Input.GetMouseButton(0))
        {
            fire = Shoot();
            //�����������
            animator.SetBool("shooting", fire);
        }
        //����
        if (Input.GetKeyDown(KeyCode.R) && bulletLeft < bulletMag) 
        {
            Reload();
        }
        fireTimer += Time.deltaTime;
    }
     bool Shoot()
    {
        //��������ж�
        if(fireTimer < fireRate )
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
        //��������
         Physics.Raycast(ShootorPoint.position, ShootorPoint.forward, out hit, range);

        if(hit.collider.gameObject.tag == "Enemy")
        {
            hit.collider.gameObject.GetComponent<EnemyHealth>().Damage(20f);
           
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

    //�ӵ�UI
    void UpdateAmmoUI()
    {
        AmmoTextUI.text = bulletLeft + "/" + bulletTotal;
    }
}
