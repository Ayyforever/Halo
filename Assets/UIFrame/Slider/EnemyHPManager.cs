using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPManager : MonoBehaviour
{
    public Slider slider;
    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // GameObject boss = GameObject.FindGameObjectWithTag("BossTag");

        slider.value = boss.GetComponent<EnemyHealth>().hp;

        if(slider.value <= 0)
        {
            Destroy(gameObject, 3f);
        }
    }
}
