using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] TextMeshProUGUI m_resumeText, m_quitText;

    GameObject m_playerCursorCanvas;

    private void Start()
    {
        m_resumeText.SetText("Press " + m_pauseButton.ToString() +
            " to resume");
        m_quitText.SetText("Press " + m_quitKey.ToString() + " to quit");

        m_playerCursorCanvas = Instantiate(
            Resources.Load<GameObject>("Prefabs/PlayerCursor"));
        m_playerCursorCanvas.SetActive(false);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        // If escape is pressed, toggle the pause meny and gametime
        if (Input.GetKeyDown(m_pauseButton))
        {
            m_isPaused = !m_isPaused;
            Time.timeScale = Convert.ToSingle(!m_isPaused);

            m_pauseMenuObject.SetActive(m_isPaused);

            m_playerCursorCanvas.SetActive(!m_isPaused);
        }
        else if (m_isPaused)
        {
            if (Input.GetKeyDown(m_quitKey))
            {
                // TODO Stop the game or return to a main menu
                Debug.Log("The quit button has been pressed");
            }
        }
    }

    void OnSceneLoaded(Scene _newScene, LoadSceneMode _mode)
    {
        if (!_newScene.name.Equals("Menu"))
        {
            m_playerCursorCanvas.SetActive(true);
        }
    }

    /// <summary>
    /// return if the game is paused or not
    /// </summary>
    /// <returns> is game paused? </returns>
    static public bool IsPaused() { return m_isPaused; }
}
