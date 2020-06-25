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
    [SerializeField] TextMeshProUGUI m_Loadingtext;

    /// <summary>
    /// Text for loading percent
    /// </summary>
    [SerializeField] TextMeshProUGUI m_loadingPercentText;

    /// <summary>
    /// Progress Slider
    /// </summary>
    [SerializeField] Slider m_slider;

    private void Start()
    {
        LevelChange.SetLoadingManager(this);
        SetAlphaToZero(m_background);
        SetAlphaToZero(m_Loadingtext);
        FadeIn();
    }

    /// <summary>
    /// Fade in assets
    /// </summary>
    public void FadeIn()
    {
        Debug.Log("Fading in");
        StartCoroutine(FadeAsset(m_background, 0.5f, true));
        StartCoroutine(FadeAsset(m_Loadingtext, 1f, true));
    }

    /// <summary>
    /// Fade out assets
    /// </summary>
    public void FadeOut()
    {
        Destroy(m_loadingPercentText);
        Destroy(m_slider);
        StartCoroutine(FadeOutLoadingScreen(m_background));
        StartCoroutine(FadeAsset(m_Loadingtext, 0.5f, false));
    }

    /// <summary>
    /// Function to set amount loaded text
    /// </summary>
    /// <param name="_amountLoaded">amount that has been loaded</param>
    public void SetLoadingPercentText(float _amountLoaded)
    {
        m_loadingPercentText.SetText(_amountLoaded + "%");
        m_slider.value = _amountLoaded;
    }
}
