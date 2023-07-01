using UnityEngine;
using UnityEngine.UI;

public class CameraFadeEffect : MonoBehaviour
{
    public float fadeDuration = 1f;  // ���뵭�����ܳ���ʱ��
    public Image fadeImage;  // Image����������ʾ���뵭��Ч��

    private bool isFading = false;  // ��־λ����ʾ��ǰ�Ƿ����ڵ��뵭��
    private float fadeTimer = 0f;  // ��¼���뵭����ʱ��

    // ��ʼ����Ч��
    public void StartFadeIn()
    {
        if (!isFading)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = Color.black;
            fadeTimer = 0f;
            isFading = true;
        }
    }

    // ��ʼ����Ч��
    public void StartFadeOut()
    {
        if (!isFading)
        {
            fadeImage.gameObject.SetActive(true);
            fadeImage.color = Color.clear;
            fadeTimer = 0f;
            isFading = true;
        }
    }

    void Update()
    {
        // ������ڵ��뵭���������͸����
        if (isFading)
        {
            fadeTimer += Time.deltaTime;

            // ���㵱ǰ͸����
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            // ����Image��͸����
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            // ���뵭��������ֹͣ��ʾImage����
            if (fadeTimer >= fadeDuration)
            {
                fadeImage.gameObject.SetActive(false);
                isFading = false;
            }
        }
    }
}
