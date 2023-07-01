using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplenishBullets : MonoBehaviour
{
    public bool playerInRange;
    // Start is called before the first frame update
    public GameObject MP;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && playerInRange)
        {
            Debug.Log("hhh");
            MP.GetComponent<WeaponController>().bulletLeft = 30;
            MP.GetComponent<WeaponController>().bulletTotal = 210;
        }
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
