using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject monsterPrefab;

    public Transform monsterTransform; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("111111");
        if (other.CompareTag("Player"))
        {
            // ����ҽ��봥����ʱ���ɹ���
            GenerateMonster();

            //Destroy(gameObject);
        }
    }

    private void GenerateMonster()
    {
        Debug.Log("trigger");
        // ������Ҫ���ɹ�����߼�������ʵ��������Ԥ���岢����λ�õ�
        Instantiate(monsterPrefab, monsterTransform.position, Quaternion.identity);
    }
}
