using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;
    

    [SerializeField] private Image fadeImage; // FadePanel 的 Image
    [SerializeField] private GameObject loadingIcon; // 可选：加载动画图标
    [SerializeField] private float fadeDuration = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            // 同时保护 FadeImage 所在的整个 GameObject
            if (fadeImage != null)
                DontDestroyOnLoad(fadeImage.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        loadingIcon?.SetActive(false);
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeOut(sceneName));
    }

    private IEnumerator FadeOut(string sceneName)
    {
        float timeElapsed = 0;

        if (loadingIcon != null)
            loadingIcon.SetActive(true);

        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, timeElapsed / fadeDuration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        // 不再调用 StopAllWithFade
        // AudioManager.Instance.StopAllWithFade();

        
        SceneManager.LoadScene(sceneName);
        yield return new WaitForSecondsRealtime(0.2f);
        
        yield return FadeIn();
    }


private IEnumerator FadeIn()
{
    float timeElapsed = 0;

    while (timeElapsed < fadeDuration)
    {
        float alpha = Mathf.Lerp(1, 0, timeElapsed / fadeDuration);
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
        timeElapsed += Time.unscaledDeltaTime;
        yield return null;
    }

    if (loadingIcon != null)
        loadingIcon.SetActive(false);
}

}
