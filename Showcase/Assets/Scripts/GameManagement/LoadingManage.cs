using UnityEngine;
using TMPro;
using static FadeIn;
using UnityEngine.UI;

public class LoadingManage : MonoBehaviour
{
    /// <summary>
    /// Background image
    /// </summary>
    [SerializeField] Image m_background;
    /// <summary>
    /// Loading text
    /// </summary>
    [SerializeField] TextMeshProUGUI m_text;

    private void Start()
    {
        LevelChange.SetLoadingManager(this);
        SetAlphaToZero(m_background);
        SetAlphaToZero(m_text);
        FadeIn();
    }

    /// <summary>
    /// Fade in assets
    /// </summary>
    public void FadeIn()
    {
        Debug.Log("Fading in");
        StartCoroutine(FadeAsset(m_background, 0.5f, true));
        StartCoroutine(FadeAsset(m_text, 1f, true));
    }

    /// <summary>
    /// Fade out assets
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(FadeOutLoadingScreen(m_background));
        StartCoroutine(FadeAsset(m_text, 0.5f, false));
    }
}
