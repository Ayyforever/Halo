using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeratoCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.GetComponentInParent<CeratoLogic>().damageBool = true;
        }
    }
}

