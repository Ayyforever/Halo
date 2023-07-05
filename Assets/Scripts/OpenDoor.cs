using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpenDoor : MonoBehaviour
{
    public bool playerInRange;

    Animator anim;

    public string itemName;
    public bool isLocked;

    public bool flag;
    public bool state;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        flag = false;
        state = false;
        isLocked = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag)
        {
            state = true;
        }
        else
        {
            state = false;
        }
        if (isLocked)
        {
            if (CheckNeeds() == true)
            {
                isLocked = false;
            }
        }
        if(Input.GetKeyDown(KeyCode.F) && playerInRange&&state==false && !isLocked)
        {
            anim.SetTrigger("Open");
            anim.SetTrigger("Opened");
            var component = GetComponent<NavMeshObstacle>();
            if(component != null)
            {
                component.enabled = false;
            }
            flag = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && playerInRange&&state && !isLocked)
        {
            anim.SetTrigger("Close");
            anim.SetTrigger("Closed");
            var component = GetComponent<NavMeshObstacle>();
            if (component != null)
            {
                component.enabled = true;
            }
            flag = false;
        }
    }

    private bool CheckNeeds()
    {
        if (itemName == "") return true;

        for(var i = InventorySystem.Instance.slotList.Count - 1; i >= 0; i--)
        {
       
            if (InventorySystem.Instance.slotList[i].transform.childCount > 0 )
            {
               
                if (InventorySystem.Instance.slotList[i].transform.GetChild(0).name == itemName)
                {
                 
                   return true;
                }
                
            }
        }
        return false;
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
