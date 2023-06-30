using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float extent; // 摇摆幅度
    public float maxExtent; // 幅度的最大值
    public float offset; // 平滑摇摆
    public MoveControl moveControl;
    public MouseControl mouseControl;
    private float time;
    // 静止
    public float maxStandY;
    public float standSwayOffset;
    // 移动
    public float maxMoveX;
    public float maxMoveY;
    public float moveSwayOffset;
    // 后座力
    public float recoilSwayX;
    public float recoilSwayY;
    public float recoilSwayZ;
    public float recoilSwayOffset;

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
            float moveSwayX = maxMoveX * Mathf.Sin(time * 6.28f);
            float moveSwayY = -maxMoveY * Mathf.Abs(Mathf.Cos(time * 6.28f));
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
            float standSwayY = maxStandY * Mathf.Sin(time * 3.14f);
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
    public void Recoil()
    {
        Vector3 move = new Vector3(recoilSwayX, -recoilSwayY, -recoilSwayZ);
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalArm + move, recoilSwayOffset);
        float xMouse = Random.Range(-1f, 1f);
        float yMouse = Random.Range(0.5f, 2f);
        mouseControl.Recoil_MouseControl(xMouse, yMouse);
    }
}
