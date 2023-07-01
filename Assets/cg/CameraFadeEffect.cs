using UnityEngine;
using UnityEngine.UI;

public class CameraFadeEffect : MonoBehaviour
{
    public float fadeDuration = 1f;  // 淡入淡出的总持续时间
    public Image fadeImage;  // Image对象，用于显示淡入淡出效果

    private bool isFading = false;  // 标志位，表示当前是否正在淡入淡出
    private float fadeTimer = 0f;  // 记录淡入淡出的时间

    // 开始淡入效果
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

    // 开始淡出效果
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
        // 如果正在淡入淡出，则更新透明度
        if (isFading)
        {
            fadeTimer += Time.deltaTime;

            // 计算当前透明度
            float alpha = Mathf.Clamp01(fadeTimer / fadeDuration);

            // 更新Image的透明度
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            // 淡入淡出结束后，停止显示Image对象
            if (fadeTimer >= fadeDuration)
            {
                fadeImage.gameObject.SetActive(false);
                isFading = false;
            }
        }
    }
}
