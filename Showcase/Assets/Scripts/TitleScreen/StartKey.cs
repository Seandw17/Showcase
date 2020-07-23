﻿using UnityEngine;
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

    Canvas m_canvas;

    private void Start()
    {
        // compose text for how to start
        string setValue = "Press left mouse button";

        if (!m_startKeyController.Equals(""))
        {
            setValue += " or" + m_startKeyController.ToString();
        }

        setValue += " to begin";

        m_startText.text = (setValue);

        m_canvas = transform.root.GetComponent<Canvas>();

        setValue = "Press " + m_howToPlayKeyBoard.ToString();
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
                m_canvas.enabled = false;
                Time.timeScale = 0;
            }
            else
            {
                GameManagerScript.SetHUDToNull();
                Time.timeScale = 1;
                m_canvas.enabled = true;
            }
        }
    }
}
