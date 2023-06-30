// Copyright (C) 2015-2021 gamevanilla - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateClean
{
    /// <summary>
    /// This component is used to provide idle progress bar animations in the demos.
    /// </summary>
    public class BulletBarAnimation : MonoBehaviour
    {
        public Image progressBar;
        public TextMeshProUGUI text;

        public float duration = 1;

        private float time = 0.0f;

        private float initialFillAmount; // 初始填充量
        void Start()
        {
            initialFillAmount = 1.0f; // 设置初始填充量为满值
            progressBar.fillAmount = initialFillAmount; // 设置血条的初始填充量
        }
        void Update()
        {
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            float currentFillAmount = progressBar.fillAmount; // 当前填充量
            float targetFillAmount = CalculateFillAmount(); // 目标填充量


            /*while (time < duration)
            {
                time += Time.deltaTime;
                //progressBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, time / duration);
                progressBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, time / duration);

            }*/

            progressBar.fillAmount = targetFillAmount; // 确保填充量最终达到目标值
            yield return null;
        }

        private float CalculateFillAmount()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Camera camera = player.GetComponentInChildren<Camera>();
            GameObject weapon = camera.transform.Find("Weapon").gameObject;
            GameObject mainWp = weapon.transform.Find("MainWp").gameObject;
            WeaponController weaponController = mainWp.GetComponent<WeaponController>();
            // 根据你的具体逻辑计算当前血量对应的填充量
            // 这里假设血量值为Health，范围为0到100，填充量范围为0到1
            float currentHealth = weaponController.bulletLeft;// 获取当前血量的逻辑，例如GameManager.Instance.GetPlayerHealth();
            return Mathf.InverseLerp(0, 30, currentHealth); // 将血量映射到填充量范围
        }
    }
}
