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

    //��Ծ�ȴ�
    private bool jumpWait;
    public float jumpHeight = 1.0f;
    public float gravityValue = -9.81f;

    [Header("�Ų���Ƶ���")]
    private float footSoundWait = 0.0f;
    private float maxFootSoundWait = 1.0f;
    public float runRate = 1.3f;

    [Header("��·�ӽ�ҡ��")]
    public float maxRotateY;
    public float moveTime = 0.0f;
    public MouseControl mouseControl;

    public Animator animator;

    [Header("��������")]
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

    //�����ƶ�
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
            
            // ���ŽŲ���
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

    //��ֹ���ﱻ�����ƶ�
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            controller.detectCollisions = false;
        }
    }

}
