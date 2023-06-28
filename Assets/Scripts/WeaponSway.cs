using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float extent; // ҡ�ڷ���
    public float maxExtent; // ���ȵ����ֵ
    public float offset; // ƽ��ҡ��
    public MoveControl moveControl;
    private float time;
    // ��ֹ
    public float maxStandY;
    private float standSwayY;
    public float standSwayOffset;
    public float maxMoveX;
    // �ƶ�
    private float moveSwayX;
    public float maxMoveY;
    private float moveSwayY;
    public float moveSwayOffset;

    [SerializeField]private Vector3 originalArm; // �ֱ۵ĳ�ʼλ��


    // Start is called before the first frame update
    void Start()
    {
        originalArm = transform.localPosition; // ��ȡ��ʼλ��
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
    }
    void Move()
    {
        time += Time.deltaTime;
        if (moveControl.GMove())
        {
            moveSwayX = maxMoveX * Mathf.Sin(time * 6.28f);
            moveSwayY = -maxMoveY * Mathf.Abs(Mathf.Cos(time * 6.28f));
            if(time >= 1.0f)
            {
                time = 0.0f;
            }
            Vector3 move = new Vector3(moveSwayX, moveSwayY, 0);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                move *= 3.0f;
            }
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalArm + move, moveSwayOffset * Time.deltaTime);
        }
        else
        {
            standSwayY = maxStandY * Mathf.Sin(time * 3.14f);
            if (time >= 2.0f)
            {
                time = 0.0f;
            }
            Vector3 move = new Vector3(0, standSwayY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalArm + move, standSwayOffset * Time.deltaTime);
        }
    }
    void Rotate()
    {
        // ����ƶ�����
        float xMove = Input.GetAxis("Mouse X") * extent;
        float yMove = Input.GetAxis("Mouse Y") * extent;
        xMove = Mathf.Clamp(xMove, -maxExtent, maxExtent);
        yMove = Mathf.Clamp(yMove, -maxExtent, maxExtent);
        Vector3 move = new Vector3(xMove, yMove, 0);

        // �仯
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalArm - move, Time.deltaTime * offset);
    }
}
