using UnityEngine;
using static LevelChange;
using TMPro;

public class StartKey : MonoBehaviour
{
    /// <summary>
    /// Button user can press to start game
    /// </summary>
    [SerializeField] string m_startKeyController;

    /// <summary>
    /// How to play menu keyboard key
    /// </summary>
    [SerializeField] KeyCode m_howToPlayKeyBoard;
    /// <summary>
    /// How to play menu controller key
    /// </summary>
    [SerializeField] string m_howToPlayKeyController;

    /// <summary>
    /// Tmpro objects
    /// </summary>
    [SerializeField] TextMeshProUGUI m_startText;
    [SerializeField] TextMeshProUGUI m_HowToPlayText;

    [SerializeField] GameObject m_howToPlay;

    private void Awake()
    {
        // compose text for how to start
        string setValue = "Press left mouse button";

        if (!m_startKeyController.Equals(""))
        {
            setValue += " or" + m_startKeyController.ToString();
        }

        setValue += " to begin";

        m_startText.SetText(setValue);


        setValue = "Press " + m_howToPlayKeyBoard.ToString();
        if (!m_howToPlayKeyController.Equals(""))
        {
            setValue += "or " + m_howToPlayKeyController.ToString(); 
        }
        setValue += " for how to play";

        m_HowToPlayText.SetText(setValue);
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
            ChangeLevel("CharacterSelection");
        }

        // Inputs for the how to play screen
        if (!m_howToPlayKeyController.Equals(""))
        {
            inputs = Input.GetKeyDown(m_howToPlayKeyBoard) ||
                Input.GetButtonDown(m_howToPlayKeyController);
        }
        else
        {
            inputs = Input.GetKeyDown(m_howToPlayKeyBoard);
        }

        if (inputs)
        {
            if (m_howToPlay.transform.parent == null)
            {
                GameManagerScript.SetNewHUD(m_howToPlay);
            }
            else
            {
                GameManagerScript.SetHUDToNull();
            }
        }
    }
}
