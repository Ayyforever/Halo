using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    private static string name = "MainPanel";
    private static string path = "Panel/MainPanel";
    public static readonly UIType uIType = new UIType(path, name);

    public MainPanel() : base(uIType)
    {

    }

    public override void OnStart()
    {
        base.OnStart();
        //UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Character").onClick.AddListener(Character);
        //UIMethods.GetInstance().GetOrAddSingleComponentInChild<Slider>(ActiveObj, "Slider").onValueChanged.AddListener(Update);
    }

    private void Character()
    {
        Scene3 scene3 = new Scene3();
        GameRoot.GetInstance().SceneControl_Root.SceneLoad(scene3.SceneName, scene3);
    }

    private void Update(float value)
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            value = 30;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
    }

    public override void OnDisable()
    {
        base.OnDisable();
    }    
    
    public override void OnDestory()
    {
        base.OnDestory();
    }
}