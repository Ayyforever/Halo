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
            // 当玩家进入触发器时生成怪物
            GenerateMonster();

            //Destroy(gameObject);
        }
    }

    private void GenerateMonster()
    {
        Debug.Log("trigger");
        // 根据需要生成怪物的逻辑，例如实例化怪物预制体并设置位置等
        Instantiate(monsterPrefab, monsterTransform.position, Quaternion.identity);
    }
}
