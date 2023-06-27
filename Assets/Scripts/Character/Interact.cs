using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Active();
        Pick();
        Fight();
    }

    //����active����
    void Active()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            animator.SetTrigger("active");
        }
    }

    //����pick up����
    void Pick()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            animator.SetTrigger("pick");
        }
    }

    //����fight����
    void Fight()
    {
        if(Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("fight");
        }
    }
}
