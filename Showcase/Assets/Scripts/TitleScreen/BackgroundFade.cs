using UnityEngine;
using UnityEngine.UI;
using static FadeIn;

public class BackgroundFade : MonoBehaviour
{
    Image m_image;

    // Start is called before the first frame update
    void Start()
    {
        m_image = GetComponent<Image>();
    }

    public void Display()
    {
        StartCoroutine(FadeAsset(m_image, 0.5f, true));
    }

    public void Remove()
    {
        StartCoroutine(FadeAsset(m_image, 0.5f, false));
    }
}
