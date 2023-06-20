using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Range(1f, 20f)]
    public float playerSpeed = 10.0f;

    //跳跃等待
    private bool jumpWait;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;


    [Header("声音设置")]
    private AudioSource audioSource;
    public AudioClip walkingSound;
    public AudioClip runingSound;

    private void Start()
    {
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
        Vector3 move = transform.right * Input.GetAxis("Horizontal") + transform.forward * Input.GetAxis("Vertical");
        if (move != Vector3.zero)
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
            return true;
        }
        else
            return false;
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
    void FootstepSource()
    {
        if (controller.isGrounded && GMove())
        {
            audioSource.clip = walkingSound;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }

}
