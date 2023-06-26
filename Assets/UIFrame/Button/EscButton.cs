using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EscButton : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameRoot.GetInstance().UIManager_Root.stack_ui.Peek().uiType.Name != "SettingsPanel")
        {
            //GameRoot.GetInstance().UIManager_Root.Pop(false);
            GameRoot.GetInstance().UIManager_Root.Push(new SettingsPanel());
            transform.SetSiblingIndex(0);
            //GameRoot.GetInstance().UIManager_Root.Push(new SettingsPanel());
        }
    }
}
