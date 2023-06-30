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

        private float initialFillAmount; // ��ʼ�����
        void Start()
        {
            initialFillAmount = 1.0f; // ���ó�ʼ�����Ϊ��ֵ
            progressBar.fillAmount = initialFillAmount; // ����Ѫ���ĳ�ʼ�����
        }
        void Update()
        {
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            float currentFillAmount = progressBar.fillAmount; // ��ǰ�����
            float targetFillAmount = CalculateFillAmount(); // Ŀ�������


            /*while (time < duration)
            {
                time += Time.deltaTime;
                //progressBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, time / duration);
                progressBar.fillAmount = Mathf.Lerp(currentFillAmount, targetFillAmount, time / duration);

            }*/

            progressBar.fillAmount = targetFillAmount; // ȷ����������մﵽĿ��ֵ
            yield return null;
        }

        private float CalculateFillAmount()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Camera camera = player.GetComponentInChildren<Camera>();
            GameObject weapon = camera.transform.Find("Weapon").gameObject;
            GameObject mainWp = weapon.transform.Find("MainWp").gameObject;
            WeaponController weaponController = mainWp.GetComponent<WeaponController>();
            // ������ľ����߼����㵱ǰѪ����Ӧ�������
            // �������Ѫ��ֵΪHealth����ΧΪ0��100���������ΧΪ0��1
            float currentHealth = weaponController.bulletLeft;// ��ȡ��ǰѪ�����߼�������GameManager.Instance.GetPlayerHealth();
            return Mathf.InverseLerp(0, 30, currentHealth); // ��Ѫ��ӳ�䵽�������Χ
        }
    }
}
