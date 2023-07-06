using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trig : MonoBehaviour
{
    public GameObject core;
    public GameObject CG3;
    void Start()
    {
        CG3.SetActive(false);
    }
 
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            core.SetActive(true);
            Destroy(gameObject);
        }
    }
}
