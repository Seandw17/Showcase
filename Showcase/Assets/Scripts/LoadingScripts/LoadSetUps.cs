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
    Button m_laptopReturn, m_Laptop1, m_laptop2, m_laptop3, m_laptop4,
        m_laptopinternet, m_returnwebpage, m_returnwebpage1, m_returnwebpage2,
        m_returnwebpage3, m_returnplayer;

    /// <summary>
    /// buttons for magazine
    /// </summary>
    [SerializeField]
    Button m_magazineExit;

    private void Start()
    {
        SceneManager.sceneLoaded += SetUp;
    }

    /// <summary>
    /// Function to handle inbetween of logic from old UI system
    /// </summary>
    /// <param name="_scene">The scene to be loaded in</param>
    /// <param name="_mode">load scene mode, use additive</param>
    void SetUp(Scene _scene, LoadSceneMode _mode = LoadSceneMode.Additive)
    {
        Debug.Assert(_mode.Equals(LoadSceneMode.Additive),
            "Load Scene Single " +
            "used, change this to additive to comply with scene structure");

        switch (_scene.name)
        {
            case "ChooseOutfit":
                Debug.Log("Performing UI setup for ChooseOutfit scene");
                FindObjectOfType<LaptopObject>().
                SetUpButtons(m_laptopReturn, m_Laptop1, m_laptop2,
                m_laptop3, m_laptop4, m_laptopinternet, m_returnwebpage,
                m_returnwebpage1, m_returnwebpage2, m_returnwebpage3,
                m_returnplayer);
                return;
            case "OfficeANDWaitingArea":
                Debug.Log("Performing UI setup for Magazine interact");
                FindObjectOfType<MagazineInteract>().SetUp(m_magazineExit);
                return;
        }
    }
}
