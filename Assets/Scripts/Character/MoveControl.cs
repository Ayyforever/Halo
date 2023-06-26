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
    public float currentAcceleration = 1.0f; // 移动加速度
    public float runRate = 2.5f;
    //跳跃等待
    private bool jumpWait;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    private Animator animator;

    [Header("声音设置")]
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

    //地面移动
    bool GMove()
    {
        // 2023/6/21 16:25新增内容
        float deltaTime = Time.deltaTime;
        float horizontal_bool = Input.GetAxis("Horizontal");
        float vertical_bool = Input.GetAxis("Vertical");
        // 速度从0开始增加
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
        // 同时前/后进+左/右走，速度会叠加，应除以根号2
        if (horizontal_bool * vertical_bool != 0)
        {
            horizontal_bool /= Mathf.Sqrt(2);
            vertical_bool /= Mathf.Sqrt(2);
        }// 2023/6/21 16:25新增内容结束


        Vector3 move = transform.right * horizontal_bool + transform.forward * vertical_bool;

        if (move != Vector3.zero)
        { 
            //跑步
            if (Input.GetKey(KeyCode.LeftShift)|| Input.GetKey(KeyCode.RightShift))
            {
                //move *= runRate; 前面已有对shift疾跑的处理，此处可不写
                animator.SetBool("run", true);
            }
            else
            {
                animator.SetBool("run", false);
            }
            controller.Move(move * deltaTime * currentSpeed); // 修改：乘的是current speed（原先是playerspeed）
            return true;
        }
        else
        {
            animator.SetBool("run", false); // 如果此处不设置回false，那么疾跑后停下来但依然按着shift键，就会一直播放疾跑动作
            return false;
        }
            
    }
    
   
   //跳跃判断
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
    //竖直受力
    void YMove()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        //跳跃检测
        Jump();
        //y轴上受到力的作用
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    //移动音效
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
