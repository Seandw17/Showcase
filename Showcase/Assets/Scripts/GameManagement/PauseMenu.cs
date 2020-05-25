using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // is the game paused
    bool isPaused;

    /// <summary>
    /// The gameobject that contains the pause menu
    /// </summary>
    [SerializeField] GameObject m_pauseMenuObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // If escape is pressed, toggle the pause meny and gametime
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            Time.timeScale = Convert.ToSingle(!isPaused);

            m_pauseMenuObject.SetActive(isPaused);
        }
    }
}
