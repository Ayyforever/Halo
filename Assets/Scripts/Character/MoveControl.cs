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

    //��Ծ�ȴ�
    private bool jumpWait;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;


    [Header("��������")]
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

    //�����ƶ�
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
