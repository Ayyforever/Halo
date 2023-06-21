using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("test");
        Debug.LogWarning("test");
        Debug.LogError("test");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(Vector3.zero, Vector3.one,Color.blue);

    }
}
