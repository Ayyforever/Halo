using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogItem : MonoBehaviour
{
    public bool playerInRange;
    public string filename;
    TextAsset dialogText;
    // Start is called before the first frame update
    void Start()
    {
        dialogText = Resources.Load(filename) as TextAsset;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {
            GuideSystem.Instance.dialog.text = dialogText.text;
            GuideSystem.Instance.PlayDialog();
            Destroy(gameObject);
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
