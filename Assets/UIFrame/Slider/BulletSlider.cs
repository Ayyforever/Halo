using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSlider : MonoBehaviour
{
    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.value = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            slider.value = 30;
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            slider.value -= 1;
        }
    }
}
