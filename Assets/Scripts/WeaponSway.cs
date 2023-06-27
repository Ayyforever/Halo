using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float extent; // ҡ�ڷ���
    public float maxExtent; // ���ȵ����ֵ
    public float offset; // ƽ��ҡ��

    [SerializeField]private Vector3 originalArm; // �ֱ۵ĳ�ʼλ��


    // Start is called before the first frame update
    void Start()
    {
        originalArm = transform.localPosition; // ��ȡ��ʼλ��
    }

    // Update is called once per frame
    void Update()
    {
        // ����ƶ�����
        float xMove = Input.GetAxis("Mouse X") * extent;
        float yMove = Input.GetAxis("Mouse Y") * extent;
        xMove = Mathf.Clamp(xMove, -maxExtent, maxExtent);
        yMove = Mathf.Clamp(yMove, -maxExtent, maxExtent);
        Vector3 move = new Vector3(xMove, yMove, 0);

        // �仯
        transform.localPosition = Vector3.Lerp(transform.localPosition,originalArm - move, Time.deltaTime * offset);

    }
}
