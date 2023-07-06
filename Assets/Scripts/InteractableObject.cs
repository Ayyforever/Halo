using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange;

    public string ItemName;
    public bool Hide;
    public string Filename;


    public string GetItemName()
    {
        return ItemName;
    }

    public string GetFilename()
    {
        return Filename;
    }

    public bool isHide()
    {
        return Hide;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && playerInRange&&SelectionManager.Instance.onTarget&&SelectionManager.Instance.selectedObject==gameObject)
        {
            if (!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddToInventory(ItemName);
                GameObject.FindGameObjectWithTag("Player").GetComponent<Interact>().Pick();

                Destroy(gameObject);
            }
            else
            {
                Debug.Log("inventory is full");
            }

            


        }

        
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
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
