using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Author: Alec

public class PauseMenu : MonoBehaviour
{
    // is the game paused
    static bool m_isPaused;

    /// <summary>
    /// The gameobject that contains the pause menu
    /// </summary>
    [SerializeField] GameObject m_pauseMenuObject;

    /// <summary>
    /// The pause key
    /// </summary>
    [SerializeField] KeyCode m_pauseButton = KeyCode.Escape;

    /// <summary>
    /// The quit key
    /// </summary>
    [SerializeField] KeyCode m_quitKey = KeyCode.Q;

    /// <summary>
    /// The text field corrosponding to an action
    /// </summary>
    [SerializeField] Text m_resumeText, m_quitText; 

    private void Start()
    {
        m_resumeText.text = ("Press " + m_pauseButton.ToString() +
            " to resume");
        m_quitText.text = ("Press " + m_quitKey.ToString() + " to quit");

        // Create the FPS counter if in editor
        if (Application.isEditor)
        {
            Instantiate(Resources.Load<GameObject>("Prefabs/FPSCounter"));
        }

        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        // if the active scene is not the title screen
        if (!SceneManager.GetActiveScene().name.Equals("TItleScreen") &&
            !SceneManager.GetActiveScene().name.Equals("PreLoad") &&
            !SceneManager.GetActiveScene().name.Equals("CharacterSelection"))
        {
            // If escape is pressed, toggle the pause meny and gametime
            if (Input.GetKeyDown(m_pauseButton))
            {
                m_isPaused = !m_isPaused;
                Time.timeScale = Convert.ToSingle(!m_isPaused);

                Canvas.ForceUpdateCanvases();

                m_pauseMenuObject.SetActive(m_isPaused);
                GameManagerScript.UIActive(!m_isPaused);
            }
            else if (m_isPaused)
            {
                if (Input.GetKeyDown(m_quitKey))
                {
                    // if quit key is pressed, quit application
                    Application.Quit();
                }

                if (Input.GetKeyDown(KeyCode.R))
                {
                    m_isPaused = !m_isPaused;
                    Time.timeScale = Convert.ToSingle(!m_isPaused);

                    m_pauseMenuObject.SetActive(m_isPaused);

                    SceneManager.LoadScene("PreLoad", LoadSceneMode.Single);
                    ConversationStore.Reset();
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }

    /// <summary>
    /// return if the game is paused or not
    /// </summary>
    /// <returns> is game paused? </returns>
    static public bool IsPaused() { return m_isPaused; }
}
