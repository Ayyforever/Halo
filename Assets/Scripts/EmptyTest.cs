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

        //��ȡtransform���
        Debug.Log(transform.position);
        //��ȡ�������
        BoxCollider bc = GetComponent<BoxCollider>();

        // GetComponentInChildren<BoxCollider>();
        //GetComponentInParent<BoxCollider>();

        gameObject.AddComponent<AudioSource>();

        //ͨ������  ��ǩ
        GameObject test = GameObject.Find("Test");
        Debug.Log(test.name);

        test = GameObject.FindWithTag("Enemy");
        Debug.Log(test.name);


        //Ԥ���� ʵ����
        GameObject g=Instantiate(Prefab,Vector3.zero,Quaternion.identity);
        Destroy(g);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
