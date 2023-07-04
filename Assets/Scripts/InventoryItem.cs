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

    
    

    private Text itemInfoUI_itemName;
    private Text itemInfoUI_itemDescription;
    private Text itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;


    public bool isRead;
    TextAsset plotText;

    private void Start()
    {
        itemInfoUI = InventorySystem.Instance.ItemInfoUI;
        itemInfoUI_itemName = itemInfoUI.transform.Find("itemName").GetComponent<Text>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("itemDescription").GetComponent<Text>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("itemFunctionality").GetComponent<Text>();

        
        

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
        InventorySystem.Instance.plotScreenUI.SetActive(false);
        Debug.Log("yesssssssss");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isRead)
            {
                plotText = Resources.Load("test") as TextAsset;
                InventorySystem.Instance.itemPlot.text = plotText.text;
                InventorySystem.Instance.inventoryScreenUI.SetActive(false);
                InventorySystem.Instance.plotScreenUI.SetActive(true);
               
            }
        }

    }

}
