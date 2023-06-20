using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 v = new Vector3(1, 1, 1);
        v = Vector3.zero;
        v = Vector3.one;
        v = Vector3.right;

        Vector3 v2 = Vector3.forward;

        Debug.Log(Vector3.Angle(v, v2));
        Debug.Log(Vector3.Distance(v, v2));
        Debug.Log(Vector3.Dot(v, v2));
        Debug.Log(Vector3.Lerp(Vector3.zero,Vector3.one,0.8f));

        Debug.Log(v.magnitude);
        Debug.Log(v.normalized);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
