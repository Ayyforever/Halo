using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform target;
    public Vector3 Offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + Offset;
        // 获取父物体的旋转角度
        float targetRotationY = target.rotation.eulerAngles.y;

        // 创建一个新的欧拉角，只在X轴上旋转，其他轴保持不变
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationY - 180f, transform.rotation.eulerAngles.z);

        // 设置物体1的旋转角度
        transform.rotation = newRotation;
    }
}
