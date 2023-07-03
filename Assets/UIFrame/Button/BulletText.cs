// Copyright (C) 2015-2021 gamevanilla - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateClean
{
    
    /// <summary>
    /// This component is associated to a progress bar's amount text and is in charge
    /// of keeping it updated with regards to the progress bar's current value.
    /// </summary>
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class BulletText : MonoBehaviour
    {
    #pragma warning disable 649
        [SerializeField]
        private Image progressBar;
#pragma warning restore 649

        
        public string Suffix;

        private TextMeshProUGUI text;

        private void Awake()
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            SetAmountText(progressBar.fillAmount);
        }

        private void Update()
        {
            SetAmountText(progressBar.fillAmount);
        }

        private void SetAmountText(float value)
        {

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            
            //GameObject camera = player.transform.Find("Camera").gameObject;
            //GameObject weapon = camera.transform.Find("Weapon").gameObject;
            //GameObject mainWp = weapon.transform.Find("MainWp").gameObject;
           // WeaponController weaponController = player.GetComponentInChildren<WeaponController>();
           // mainWp.GetComponent<WeaponController>();
            //int Left = player.GetComponentInChildren<WeaponController>().bulletLeft;
            //int Total = player.GetComponentInChildren<WeaponController>().bulletTotal;

            //text.text = $"{Left}{Suffix}{Total}";
        }
    }
}