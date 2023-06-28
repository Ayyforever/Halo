using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickLight : MonoBehaviour
{
    public GameObject flickLight;
    private float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(flickLight.activeSelf == true && time >= 0.5f)
        {
            flickLight.SetActive(false);
            time = 0.0f;
        }
        else if(flickLight.activeSelf == false && time >= 0.025f)
        {
            flickLight.SetActive(true);
            time = 0.0f;
        }
    }
}
