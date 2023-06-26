using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public void Onclick()
    {
        Scene1 scene1 = new Scene1();
        GameRoot.GetInstance().SceneControl_Root.SceneLoad(scene1.SceneName, scene1);
        GameRoot.GetInstance().UIManager_Root.Push(new StartPanel());
    }
}
