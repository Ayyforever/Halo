using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTest : MonoBehaviour
{
    public GameObject Cube;
    // Start is called before the first frame update

    public GameObject Prefab;
    void Start()
    {
        GameObject go = this.gameObject;
        Debug.Log(go.name);
        Debug.Log(gameObject.tag);
        Debug.Log(gameObject.layer);

        Debug.Log(Cube.name);
        Debug.Log(Cube.activeInHierarchy);
        Debug.Log(Cube.activeSelf);

        //获取transform组件
        Debug.Log(transform.position);
        //获取其他组件
        BoxCollider bc = GetComponent<BoxCollider>();

        // GetComponentInChildren<BoxCollider>();
        //GetComponentInParent<BoxCollider>();

        gameObject.AddComponent<AudioSource>();

        //通过名称  标签
        GameObject test = GameObject.Find("Test");
        Debug.Log(test.name);

        test = GameObject.FindWithTag("Enemy");
        Debug.Log(test.name);


        //预设体 实例化
        GameObject g=Instantiate(Prefab,Vector3.zero,Quaternion.identity);
        Destroy(g);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
