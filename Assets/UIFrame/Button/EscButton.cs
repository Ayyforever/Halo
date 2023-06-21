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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Scene3 scene3 = new Scene3();
            GameRoot.GetInstance().SceneControl_Root.SceneLoad(scene3.SceneName, scene3);
        }
    }
}
