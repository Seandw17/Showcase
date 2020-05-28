using System;
using UnityEngine;

// Author: Alec

public class PauseMenu : MonoBehaviour
{
    // is the game paused
    static bool m_isPaused;

    /// <summary>
    /// The gameobject that contains the pause menu
    /// </summary>
    [SerializeField] GameObject m_pauseMenuObject;

    // Update is called once per frame
    void Update()
    {
        // If escape is pressed, toggle the pause meny and gametime
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_isPaused = !m_isPaused;
            Time.timeScale = Convert.ToSingle(!m_isPaused);

            m_pauseMenuObject.SetActive(m_isPaused);
        }
    }

    /// <summary>
    /// return if the game is paused or not
    /// </summary>
    /// <returns> is game paused? </returns>
    static public bool IsPaused() { return m_isPaused; }
}
