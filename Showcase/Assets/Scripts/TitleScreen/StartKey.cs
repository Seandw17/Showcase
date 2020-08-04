using UnityEngine;
using static LevelChange;
using UnityEngine.UI;

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
    [SerializeField] Text m_startText;
    [SerializeField] Text m_HowToPlayText;

    [SerializeField] GameObject m_howToPlay;

    private void Start()
    {
        // compose text for how to start
        string setValue = "Click here";

        if (!m_startKeyController.Equals(""))
        {
            setValue += " or press" + m_startKeyController.ToString();
        }

        setValue += " to begin";

        m_startText.text = (setValue);

        setValue = "Click here, or press " + m_howToPlayKeyBoard.ToString();
        if (!m_howToPlayKeyController.Equals(""))
        {
            setValue += "or " + m_howToPlayKeyController.ToString(); 
        }
        setValue += " for how to play";

        m_HowToPlayText.text = (setValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        // Check if we need to check for button
        bool inputs;
        if (!m_startKeyController.Equals(""))
        {
            inputs = 
                Input.GetButtonDown(m_startKeyController);

            // if true, change level
            if (inputs)
            {
                Time.timeScale = 1;
                ChangeLevel("CharacterSelection");
            }
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
            if (!m_howToPlay.activeSelf)
            {
                m_howToPlay.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                m_howToPlay.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
