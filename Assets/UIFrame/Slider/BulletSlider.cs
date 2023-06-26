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
        slider.value = 30;
    }

    // Update is called once per frame
    void Update()
    {
        //Transform target = transform.Find("Player/ÉãÏñ»ú/Weapon/MainWp");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Camera camera = player.GetComponentInChildren<Camera>();
        GameObject weapon = camera.transform.Find("Weapon").gameObject;
        GameObject mainWp = weapon.transform.Find("MainWp").gameObject;
        WeaponController weaponController = mainWp.GetComponent<WeaponController>();

        slider.value = weaponController.bulletLeft;

    }
}
