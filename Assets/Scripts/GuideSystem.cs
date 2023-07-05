using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideSystem : MonoBehaviour
{
    public static GuideSystem Instance { get; set; }

    public GameObject guideScreenUI;

    public Text dialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayDialog()
    {
        guideScreenUI.SetActive(true);
        Invoke("show", 4);

    }

    public void show()
    {
        guideScreenUI.SetActive(false);
    }
}
