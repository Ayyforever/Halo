using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeDamage : MonoBehaviour
{
    public GameObject Explosion;

    //��ը��Χ
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
            //��Ѫ
            if (nearby.GetComponent<EnemyHealth>() != null)
            {
                nearby.GetComponent<EnemyHealth>().Damage(50f);
            }
        }
        GameObject ExplosionOb = Instantiate(Explosion, transform.position, transform.rotation);
        Destroy(ExplosionOb, 1.8f);
        Destroy(gameObject);
    }
}
