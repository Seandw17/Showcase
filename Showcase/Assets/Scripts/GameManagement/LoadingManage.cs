using UnityEngine;
using static FadeIn;
using UnityEngine.UI;

public class LoadingManage : MonoBehaviour
{
    /// <summary>
    /// Background image
    /// </summary>
    [SerializeField] Image m_background;

    /// <summary>
    /// Text for loading percent
    /// </summary>
    [SerializeField] Text m_loadingPercentText;

    /// <summary>
    /// Progress Slider
    /// </summary>
    [SerializeField] Slider m_slider;

    /// <summary>
    /// Image that will spin to show we are loading
    /// </summary>
    [SerializeField] Transform m_spinner;

    private void Start()
    {
        LevelChange.SetLoadingManager(this);
        SetAlphaToZero(m_background);
        FadeIn();
        Cursor.visible = false;
    }

    /// <summary>
    /// Fade in assets
    /// </summary>
    public void FadeIn()
    {
        Debug.Log("Fading in");
        StartCoroutine(FadeAsset(m_background, 0.5f, true));
    }

    /// <summary>
    /// Fade out assets
    /// </summary>
    public void FadeOut()
    {
        Destroy(m_loadingPercentText);
        Destroy(m_slider);
        StartCoroutine(FadeOutLoadingScreen(m_background));
    }

    /// <summary>
    /// Function to set amount loaded text
    /// </summary>
    /// <param name="_amountLoaded">amount that has been loaded</param>
    public void SetLoadingPercentText(float _amountLoaded)
    {
        if (_amountLoaded > m_slider.value)
        {
            Spin();
        }

        m_loadingPercentText.text = (_amountLoaded.ToString("n1") + "%");
        m_slider.value = _amountLoaded;
    }

    /// <summary>
    /// Spin the progress spinner
    /// </summary>
    void Spin()
    {
        // Spin the spinner, show something is happening
        m_spinner.transform.Rotate(0, 0, -20, Space.Self);
    }
}
