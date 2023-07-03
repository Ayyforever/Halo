using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [Range(1f,25f)]
    public float playerSpeed = 5.0f;

    //跳跃等待
    private bool jumpWait;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    [Header("脚步音频间隔")]
    private float footSoundWait = 0.0f;
    private float maxFootSoundWait = 1.0f;
    public float runRate = 1.3f;

    [Header("走路视角摇晃")]
    public float maxRotateY;
    public float moveTime = 0.0f;
    public MouseControl mouseControl;

    public Animator animator;

    [Header("声音设置")]
    public AudioSource audioSource;
    public AudioClip[] walkingSound = new AudioClip[5];

    private void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        
    }
    void Update()
    {

        GMove();
        YMove();
        //FootstepSource();

    }

    //地面移动
    public bool GMove()
    {
        //if (!groundedPlayer) { return false; }
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");
        if(xMove != 0.0f && yMove != 0.0f)
        {
            xMove /= Mathf.Sqrt(2);
            yMove /= Mathf.Sqrt(2);
        }
        Vector3 move = transform.right * xMove + transform.forward * yMove;
        
        if (move != Vector3.zero)
        {
            if (footSoundWait < maxFootSoundWait)
            {
                footSoundWait += Time.deltaTime;
            }
            if(moveTime < 1.0f)
            {
                moveTime += Time.deltaTime;
            }
            float moveRotateY = 0;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                move *= runRate;
                maxFootSoundWait = 0.5f;

                moveRotateY = -2f * maxRotateY * Mathf.Sin(moveTime * 12.56f);

                animator.SetBool("run",true);
            }
            else
            {
                moveRotateY = -maxRotateY * Mathf.Sin(moveTime * 6.28f);
                maxFootSoundWait = 1.0f;
                animator.SetBool("run", false);
            }
            controller.Move(move * Time.deltaTime * playerSpeed);
            mouseControl.Move_MouseControl(moveRotateY);
            
            // 播放脚步声
            if (footSoundWait >= maxFootSoundWait)
            {
                MoveSoundPlay();
                footSoundWait = 0.0f;
            }
            if(moveTime >= 1.0f) { moveTime = 0.0f; }
            
            return true;
        }
        else
        {
            footSoundWait = 0.0f;
            return false;
        }
    }

    void MoveSoundPlay()
    {
        int n = Random.Range(0, walkingSound.Length);
        audioSource.clip = walkingSound[n];
        audioSource.Play();
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

    //防止人物被怪物推动
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            controller.detectCollisions = false;
        }
    }

}
