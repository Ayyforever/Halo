using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySTrigger : MonoBehaviour
{
    //����ˢ������
    public int n;
    public GameObject monsterPrefab;
    public Transform monsterTransform;

    //����ٴξ���ʱˢ
    private int trigger =0;
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

        if (other.CompareTag("Player"))
        {
            trigger += 1;
        }
        if(trigger >= 2)
        {
            GenerateMonster();

            Destroy(gameObject);
        }
    }

    private void GenerateMonster()
    {
        for (int i = 0; i < n; i++)
        {
            // ������Ҫ���ɹ�����߼�������ʵ��������Ԥ���岢����λ�õ�
            Instantiate(monsterPrefab, monsterTransform.position, Quaternion.identity);
        }
    }
}

