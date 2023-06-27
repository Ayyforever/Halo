using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public void Onclick()
    {
        GameRoot.GetInstance().UIManager_Root.Pop(false);
        Cursor.lockState = CursorLockMode.Locked;
    }
}
