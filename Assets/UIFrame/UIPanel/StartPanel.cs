using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private static string name = "StartPanel";
    private static string path = "Panel/StartPanel";
    public static readonly UIType uIType = new UIType(path, name);

    public StartPanel() : base(uIType)
    { 

    }

    public override void OnStart()
    {
        base.OnStart();
        UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Back").onClick.AddListener(Back);
        UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Load").onClick.AddListener(Load);
    }

    private void Back()
    {
        //GameRoot.GetInstance().UIManager_Root.Pop(false);
        #if UNITY_EDITOR
            // 在Unity编辑器中运行时，停止播放模式
            UnityEditor.EditorApplication.isPlaying = false;
        #else
        // 在发布的应用程序中调用Application.Quit()函数
        Application.Quit();
        #endif
    }

    private void Load()
    {
        Scene2 scene2 = new Scene2();
        GameRoot.GetInstance().SceneControl_Root.SceneLoad(scene2.SceneName, scene2);
        GameRoot.GetInstance().UIManager_Root.Push(new MainPanel());      
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        Debug.Log("StartPanel Back");
        base.OnDisable();
    }

    public override void OnDestory()
    {
        base.OnDestory();
    }
}
