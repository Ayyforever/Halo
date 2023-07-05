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
        // ��ȡ���������ת�Ƕ�
        float targetRotationY = target.rotation.eulerAngles.y;

        // ����һ���µ�ŷ���ǣ�ֻ��X������ת�������ᱣ�ֲ���
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, targetRotationY - 180f, transform.rotation.eulerAngles.z);

        // ��������1����ת�Ƕ�
        transform.rotation = newRotation;
    }
}
