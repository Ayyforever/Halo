using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;     // 存储四种不同怪物的预制体
    public float spawnInterval = 10f;        // 刷新间隔时间

    public Transform transform;

    private float timer = 0f;               // 计时器

    private void Start()
    {

    }
    private void Update()
    {
        // 更新计时器
        timer += Time.deltaTime;

        // 如果计时器超过刷新间隔时间，刷新怪物并重置计时器
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
        // 随机选择一个怪物预制体
        int randomIndex = Random.Range(0, monsterPrefabs.Length);
        GameObject monsterPrefab = monsterPrefabs[randomIndex];

        // 在随机位置生成怪物
        
        {
            Instantiate(monsterPrefab, transform.position, Quaternion.identity);
        }
    }

}
