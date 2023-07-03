using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWp : MonoBehaviour
{
    public Animator animator;

    //����
    public float power = 0f;
    private bool grenadeMode;

    public float fireTimer =1.5f;

    public Transform firePoint;
    public GameObject grenadePrefab;

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
            timer += Time.deltaTime;
        }
        if (timer >= 20f && grenadeMode == true)
        {
            grenadeMode = false;
            //������������ű�
            gameObject.GetComponent<WeaponController>().enabled = true;
            //���ü�ʱ��
            timer = 0;
        }
        fireTimer += Time.deltaTime;
    }


    void Grenade()
    {
        if (Input.GetKeyDown(KeyCode.Q) && power >= 0f)
        {
            //�ر���������ű�
            gameObject.GetComponent<WeaponController>().enabled = false;
            grenadeMode = true;
        }
    }

    void Shoot()
    {
        if(fireTimer < 1.5f)
        {
            return;
        }
        animator.SetTrigger("fight");
        //�����ӵ�
        // ʵ������Ԥ����
        GameObject grenade = Instantiate(grenadePrefab, firePoint.position,firePoint.rotation);

        // ��ȡ�񵯸������
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Ӧ�÷�������
        rb.AddForce(firePoint.forward * 10f, ForceMode.Impulse);


        fireTimer = 0f;
    }

}
