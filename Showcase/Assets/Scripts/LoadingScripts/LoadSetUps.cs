using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Class to manage certain on load logic
/// </summary>
public class LoadSetUps : MonoBehaviour
{
    /// <summary>
    /// Buttons for laptop
    /// </summary>
    [SerializeField]
    Button m_laptopReturn, m_Laptop1, m_laptop2, m_laptop3, m_laptop4, m_laptopinternet, m_returnwebpage, m_returnwebpage1, m_returnwebpage2, m_returnwebpage3, m_returnplayer;

    /// <summary>
    /// buttons for magazine
    /// </summary>
    [SerializeField]
    Button m_magazineExit;

    private void Start()
    {
        SceneManager.sceneLoaded += SetUpLaptop;
        SceneManager.sceneLoaded += SetUpMagazine;
    }

    /// <summary>
    /// Set up laptop buttons
    /// </summary>
    /// <param name="_scene">scene to be loaded</param>
    /// <param name="_mode">loading mode</param>
    void SetUpLaptop(Scene _scene, LoadSceneMode _mode )
    {
        if (_scene.name.Equals("ChooseOutfit"))
        {
            FindObjectOfType<LaptopObject>().
                SetUpButtons(m_laptopReturn, m_Laptop1, m_laptop2,
                m_laptop3, m_laptop4, m_laptopinternet, m_returnwebpage, m_returnwebpage1, m_returnwebpage2, m_returnwebpage3, m_returnplayer);
        }
    }

    /// <summary>
    /// Set up magazine buttons
    /// </summary>
    /// <param name="_scene">scene to be loaded</param>
    /// <param name="_mode">loading mode</param>
    void SetUpMagazine(Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.name.Equals("OfficeANDWaitingArea"))
        {
            FindObjectOfType<MagazineInteract>().SetUp(m_magazineExit);
        }
    }
}
