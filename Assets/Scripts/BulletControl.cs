using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    private float existTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0, Space.Self);
        existTime += Time.deltaTime;
        if(existTime >= lifeTime) { Destroy(this.gameObject); }
    }
    void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
