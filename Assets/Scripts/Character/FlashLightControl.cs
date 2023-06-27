using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightControl : MonoBehaviour
{
    public GameObject flashLight;
    private float disappearTime = 0.0f;
    private bool lightActive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lightActive = flashLight.activeSelf;
        if(lightActive == true && disappearTime == 0.0f)
        {
            lightActive = false;
            disappearTime = Time.deltaTime;
        }
        if(disappearTime != 0.0f)
        {
            disappearTime += Time.deltaTime;
            if(disappearTime >= 0.1f)
            {
                disappearTime = 0.0f;
                flashLight.SetActive(false);
            }
        }

    }
}
