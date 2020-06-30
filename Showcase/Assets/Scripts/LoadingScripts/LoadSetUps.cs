using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSetUps : MonoBehaviour
{
    [SerializeField]
    Button m_laptopReturn, m_Laptop1, m_laptop2, m_laptop3, m_laptop4;

    [SerializeField]
    Button m_magazineExit;

    private void Start()
    {
        SceneManager.sceneLoaded += SetUpLaptop;
        SceneManager.sceneLoaded += SetUpMagazine;
    }

    void SetUpLaptop(Scene _scene, LoadSceneMode _mode )
    {
        if (_scene.name.Equals("ChooseOutfit"))
        {
            FindObjectOfType<LaptopObject>().
                SetUpButtons(m_laptopReturn, m_Laptop1, m_laptop2,
                m_laptop3, m_laptop4);
        }
    }

    void SetUpMagazine(Scene _scene, LoadSceneMode _mode)
    {
        if (_scene.name.Equals("OfficeANDWaitingArea"))
        {
            FindObjectOfType<MagazineInteract>().SetUp(m_magazineExit);
        }
    }
}
