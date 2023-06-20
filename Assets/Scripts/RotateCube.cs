using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 rotate = new Vector3(0, 30, 0);
        Quaternion quaternion = Quaternion.identity;
        quaternion = Quaternion.Euler(rotate);

        quaternion = Quaternion.LookRotation(new Vector3(0, 0, 0));



        rotate = quaternion.eulerAngles;



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
