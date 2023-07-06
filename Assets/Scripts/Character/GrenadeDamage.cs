using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDamage : MonoBehaviour
{
    public GameObject Explosion;

    //±¬Õ¨·¶Î§
    public float explosionRadius = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider nearby in colliders)
        {
            //¿ÛÑª
            if (nearby.GetComponent<EnemyHealth>() != null)
            {
                float d = Vector3.Distance(nearby.transform.position, transform.position);
                float damage =  100f;
                nearby.GetComponent<EnemyHealth>().Damage(damage);
            }
        }
        GameObject ExplosionOb = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(ExplosionOb, 1.8f);
        Destroy(gameObject);
    }
}
