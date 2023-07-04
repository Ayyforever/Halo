using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GobblerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<GobblerLogic>().damageBool = true;
        }
    }
}
