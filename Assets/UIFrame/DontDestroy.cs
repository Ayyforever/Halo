using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 加载时不销毁的对象
public class DontDestroy : MonoBehaviour
{


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
