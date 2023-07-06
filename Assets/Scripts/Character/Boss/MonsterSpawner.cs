using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;     // �洢���ֲ�ͬ�����Ԥ����
    public float spawnInterval = 10f;        // ˢ�¼��ʱ��

    public Transform transform;

    private float timer = 0f;               // ��ʱ��

    private void Start()
    {

    }
    private void Update()
    {
        // ���¼�ʱ��
        timer += Time.deltaTime;

        // �����ʱ������ˢ�¼��ʱ�䣬ˢ�¹��ﲢ���ü�ʱ��
        if (timer >= spawnInterval)
        {
            for (int n = 0; n < 2; n++)
            {
                SpawnMonster();
            }
            timer = 0f;
        }
    }

    private void SpawnMonster()
    {
        // ���ѡ��һ������Ԥ����
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject monsterPrefab = monsterPrefabs[randomIndex];

        // �����λ�����ɹ���
        
        {
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        }
    }

}
