﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Michsky.UI.Shift
{
    public class SliderManager : MonoBehaviour
    {
        [Header("TEXTS")]
        public TextMeshProUGUI valueText;

        [Header("SAVING")]
        public bool enableSaving = false;
        public string sliderTag = "Tag Text";
        public float defaultValue = 1;

        [Header("SETTINGS")]
        public bool usePercent = false;
        public bool showValue = true;
        public bool useRoundValue = false;

        Slider mainSlider;
        float saveValue;

        void Start()
        {
            mainSlider = gameObject.GetComponent<Slider>();

            if (showValue == false)
                valueText.enabled = false;

            if (enableSaving == true)
            {
                if (PlayerPrefs.HasKey(sliderTag + "SliderValue") == false)
                    saveValue = defaultValue;
                else
                    saveValue = PlayerPrefs.GetFloat(sliderTag + "SliderValue");

                mainSlider.value = saveValue;

                mainSlider.onValueChanged.AddListener(delegate
                {
                    saveValue = mainSlider.value;
                    PlayerPrefs.SetFloat(sliderTag + "SliderValue", saveValue);
                });
            }
        }

        void Update()
        {
            //Transform target = transform.Find("Player/摄像机/Weapon/MainWp");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Camera camera = player.GetComponentInChildren<Camera>();
            GameObject weapon = camera.transform.Find("Weapon").gameObject;
            GameObject mainWp = weapon.transform.Find("MainWp").gameObject;
            WeaponController weaponController = mainWp.GetComponent<WeaponController>();



            if (useRoundValue == true)
            {
                if (usePercent == true)
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString() + "%";

                else
                    valueText.text = Mathf.Round(mainSlider.value * 1.0f).ToString() + " / " + (weaponController.bulletTotal).ToString();
            }

            else
            {
                if (usePercent == true)
                    valueText.text = mainSlider.value.ToString("F1") + "%";

                else
                    valueText.text = mainSlider.value.ToString("F1");
            }
        }
    }
}