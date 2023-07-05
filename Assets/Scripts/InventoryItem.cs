using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler
{
    // Start is called before the first frame update
    public bool isTrashable;

    private GameObject itemInfoUI;


    string filename;

    private Text itemInfoUI_itemName;
    private Text itemInfoUI_itemDescription;
    private Text itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;


    public bool isRead;
   

    TextAsset plotText;

    public bool isConsumable;
    public float hpEffect;

    GameObject player;
    PlayerHealth playerHealth;
    float maxHP = 100;
    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;
        itemInfoUI_itemName = itemInfoUI.transform.Find("itemName").GetComponent<Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("itemDescription").GetComponent<Text>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("itemFunctionality").GetComponent<Text>();


        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

   

    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isRead)
            {

                filename = SelectionManager.Instance.selectionObjectFile;
                plotText = Resources.Load(filename) as TextAsset;
                InventorySystem.Instance.itemPlot.text = plotText.text;
                InventorySystem.Instance.inventoryScreenUI.SetActive(false);
                
                InventorySystem.Instance.plotScreenUI.SetActive(true);
               
            }else if(isConsumable){
                
                healthEffectCalculation(hpEffect);
                Destroy(gameObject);
            }
        }

    }

    private void healthEffectCalculation(float healthEffect)
    {
        
        if (healthEffect != 0)
        {
            if ((playerHealth.hp + healthEffect) > maxHP)
            {
                playerHealth.hp = maxHP;
            }
            else
            {
                playerHealth.hp = playerHealth.hp + healthEffect;
            }
        }
        itemInfoUI.SetActive(false);
        InventorySystem.Instance.inventoryScreenUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        InventorySystem.Instance.isOpen = false;
    }
}
