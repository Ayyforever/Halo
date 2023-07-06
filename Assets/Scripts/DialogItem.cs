using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogItem : MonoBehaviour
{
    public bool playerInRange;
    public string filename;
    public GameObject dialogActivate;
    public GameObject dialogForbid;
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
            if (dialogActivate != null) { dialogActivate.SetActive(true); }
            if (dialogForbid != null) { dialogForbid.SetActive(false); }
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
