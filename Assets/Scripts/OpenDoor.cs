using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            anim.SetTrigger("Open");
            anim.SetTrigger("Opened");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Close");
            anim.SetTrigger("Closed");
        }
    }
}
