using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : BasePanel
{
    private static string name = "SettingsPanel";
    private static string path = "Panel/SettingsPanel";
    public static readonly UIType uIType = new UIType(path, name);

    public SettingsPanel() : base(uIType)
    {

    }

    public override void OnStart()
    {
        base.OnStart();
        GameRoot.GetInstance().transform.SetAsFirstSibling();
        //UIMethods.GetInstance().GetOrAddSingleComponentInChild<Button>(ActiveObj, "Keyboard Shortcut Key").onClick.AddListener(Character);
        //UIMethods.GetInstance().GetOrAddSingleComponentInChild<Slider>(ActiveObj, "Slider").onValueChanged.AddListener(Update);
    }

    private void Update(float value)
    {
        GameRoot.GetInstance().transform.SetAsFirstSibling();
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
