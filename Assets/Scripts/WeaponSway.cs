using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float extent; // 摇摆幅度
    public float maxExtent; // 幅度的最大值
    public float offset; // 平滑摇摆

    [SerializeField]private Vector3 originalArm; // 手臂的初始位置


    // Start is called before the first frame update
    void Start()
    {
        originalArm = transform.localPosition; // 获取初始位置
    }

    // Update is called once per frame
    void Update()
    {
        // 鼠标移动幅度
        float xMove = Input.GetAxis("Mouse X") * extent;
        float yMove = Input.GetAxis("Mouse Y") * extent;
        xMove = Mathf.Clamp(xMove, -maxExtent, maxExtent);
        yMove = Mathf.Clamp(yMove, -maxExtent, maxExtent);
        Vector3 move = new Vector3(xMove, yMove, 0);

        // 变化
        transform.localPosition = Vector3.Lerp(transform.localPosition,originalArm - move, Time.deltaTime * offset);

    }
}
