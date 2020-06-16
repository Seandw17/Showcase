using UnityEngine;
using static LevelChange;
using TMPro;

public class StartKey : MonoBehaviour
{
    /// <summary>
    /// Button user can press to start game
    /// </summary>
    [SerializeField] string m_startKeyController;

    private void Awake()
    {
        // compose text for how to start
        string setValue = "Press left mouse button";

        if (!m_startKeyController.Equals(""))
        {
            setValue += " or" + m_startKeyController.ToString();
        }

        setValue += " to begin";

        GetComponent<TextMeshProUGUI>().SetText(setValue);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if we need to check for button
        bool inputs;
        if (!m_startKeyController.Equals(""))
        {
            inputs = Input.GetMouseButtonDown(0) ||
                Input.GetButtonDown(m_startKeyController);
        }
        else
        {
            inputs = Input.GetMouseButtonDown(0);
        }

        // if true, change level
        if (inputs)
        {
            ChangeLevel("ChooseOutfit");
        }
    }
}
