// Copyright (C) 2015-2021 gamevanilla - All rights reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement.
// A Copy of the Asset Store EULA is available at http://unity3d.com/company/legal/as_terms.

using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UltimateClean
{
    /// <summary>
    /// This component is used to provide idle slider animations in the demos.
    /// </summary>
    public class SliderAnimation : MonoBehaviour
    {
        public Image progressBar;

        public SlicedFilledImage sliced;

        public TextMeshProUGUI text;

        public float duration = 1;

        //private float time = 0.0f;

        private float initialFillAmount; // 初始填充量
        void Start()
        {
            initialFillAmount = 0.0f; // 设置初始填充量为满值
            progressBar.fillAmount = initialFillAmount; // 设置血条的初始填充量
            sliced.fillAmount = initialFillAmount;
        }
        void Update()
        {
            Animate();
        }

        private void Animate()
        {
            //float currentFillAmount = progressBar.fillAmount; // 当前填充量
            if (GameObject.FindGameObjectWithTag("Player"))
            {
                float targetFillAmount = CalculateFillAmount(); // 目标填充量
                progressBar.fillAmount = targetFillAmount; // 确保填充量最终达到目标值
                sliced.fillAmount = targetFillAmount;
            }

            /*while (time < duration)
            {
                time += Time.deltaTime;
                //progressBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, time / duration);
                progressBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, time / duration);

            }*/
        }

        private float CalculateFillAmount()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Camera camera = player.GetComponentInChildren<Camera>();
            GameObject weapon = camera.transform.Find("Weapon").gameObject;
            GameObject mainWp = weapon.transform.Find("MainWp").gameObject;
            SkillWp skill = mainWp.GetComponent<SkillWp>();
            // 根据你的具体逻辑计算当前血量对应的填充量
            // 这里假设血量值为Health，范围为0到100，填充量范围为0到1
            float currentHealth = skill.power;// 获取当前血量的逻辑，例如GameManager.Instance.GetPlayerHealth();
            return Mathf.InverseLerp(0, skill.powerLimit, currentHealth); // 将血量映射到填充量范围
        }
    }
}