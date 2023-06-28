using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float extent; // 摇摆幅度
    public float maxExtent; // 幅度的最大值
    public float offset; // 平滑摇摆
    public MoveControl moveControl;
    private float time;
    // 静止
    public float maxStandY;
    private float standSwayY;
    public float standSwayOffset;
    public float maxMoveX;
    // 移动
    private float moveSwayX;
    public float maxMoveY;
    private float moveSwayY;
    public float moveSwayOffset;

    [SerializeField]private Vector3 originalArm; // 手臂的初始位置


    // Start is called before the first frame update
    void Start()
    {
        originalArm = transform.localPosition; // 获取初始位置
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
        // 鼠标移动幅度
        float xMove = Input.GetAxis("Mouse X") * extent;
        float yMove = Input.GetAxis("Mouse Y") * extent;
        xMove = Mathf.Clamp(xMove, -maxExtent, maxExtent);
        yMove = Mathf.Clamp(yMove, -maxExtent, maxExtent);
        Vector3 move = new Vector3(xMove, yMove, 0);

        // 变化
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalArm - move, Time.deltaTime * offset);
    }
}
