using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����ʱ�����ٵĶ���
public class DontDestroy : MonoBehaviour
{


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
