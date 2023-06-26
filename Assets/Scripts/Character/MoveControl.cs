using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Range(1f, 20f)]
    public float playerSpeed = 3.0f;
    public float currentSpeed = 0.0f;
    public float currentAcceleration = 1.0f; // �ƶ����ٶ�
    public float runRate = 2.5f;
    //��Ծ�ȴ�
    private bool jumpWait;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    private Animator animator;

    [Header("��������")]
    private AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip runingSound;

    private void Start()
    {
        animator =GameObject.Find("MainWp"). GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        GMove();
        YMove();
        
       // FootstepSource();
        
    }

    //�����ƶ�
    bool GMove()
    {
        // 2023/6/21 16:25��������
        float deltaTime = Time.deltaTime;
        float horizontal_bool = Input.GetAxis("Horizontal");
        float vertical_bool = Input.GetAxis("Vertical");
        // �ٶȴ�0��ʼ����
        if (horizontal_bool != 0 || vertical_bool != 0)
        {
            if (currentSpeed <= playerSpeed)
            {
                currentSpeed += 5.0f * currentAcceleration * deltaTime;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed += 8.0f * currentAcceleration * deltaTime;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    currentSpeed += 10.0f * currentAcceleration * deltaTime;
                }
                else
                {
                    currentSpeed -= 5.0f * currentAcceleration * deltaTime;
                }
                currentSpeed = Mathf.Clamp(currentSpeed, playerSpeed, playerSpeed * runRate);
            }
        }
        else
        {
            currentSpeed = 0.3f * playerSpeed;
        }
        // ͬʱǰ/���+��/���ߣ��ٶȻ���ӣ�Ӧ���Ը���2
        if (horizontal_bool * vertical_bool != 0)
        {
            horizontal_bool /= Mathf.Sqrt(2);
            vertical_bool /= Mathf.Sqrt(2);
        }// 2023/6/21 16:25�������ݽ���


        Vector3 move = transform.right * horizontal_bool + transform.forward * vertical_bool;

        if (move != Vector3.zero)
        { 
            //�ܲ�
            if (Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift))
            {
                //move *= runRate; ǰ�����ж�shift���ܵĴ����˴��ɲ�д
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }
            controller.Move(move * deltaTime * currentSpeed); // �޸ģ��˵���current speed��ԭ����playerspeed��
            return true;
        }
        else
        {
            animator.SetBool("run", false); // ����˴������û�false����ô���ܺ�ͣ��������Ȼ����shift�����ͻ�һֱ���ż��ܶ���
            return false;
        }
            
    }
    
   
   //��Ծ�ж�
   void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                jumpWait = false;

            }
            else
            {
                jumpWait = true;
            }
        }
        if (groundedPlayer && jumpWait)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumpWait = false;
        }
    }
    //��ֱ����
    void YMove()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //��Ծ���
        Jump();
        //y�����ܵ���������
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    //�ƶ���Ч
    //void FootstepSource()
    //{
    //    if (controller.isGrounded && GMove())
    //    {
    //        audioSource.clip = walkingSound;
    //        if (!audioSource.isPlaying)
    //        {
    //            audioSource.Play();
    //        }
    //    }
    //}

}
