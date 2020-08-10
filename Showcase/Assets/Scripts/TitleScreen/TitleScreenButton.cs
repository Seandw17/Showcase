using UnityEngine;
using static LevelChange;

public class TitleScreenButton : MonoBehaviour
{
    /// <summary>
    /// Actions that can occur on click
    /// </summary>
    enum action
    {
        START,
        DISPLAY,
        BACK,
        QUIT
    }

    /// <summary>
    /// What this element should do on click
    /// </summary>
    [SerializeField]
    action m_onClickAction = action.START;

    [SerializeField] GameObject m_howToPlay = null;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
    }

    public void Trigger()
    {
        switch (m_onClickAction)
        {
            case action.DISPLAY:
                Time.timeScale = 0;
                Debug.Assert(m_howToPlay != null,
                    "Set the gameobject for how to play");
                m_howToPlay.SetActive(true);
                return;
            case action.QUIT:
                Application.Quit();
                break;
            case action.START:
                Time.timeScale = 1;
                ChangeLevel("CharacterSelection");
                break;
            case action.BACK:
                Debug.Assert(m_howToPlay != null,
                    "Set GameObject for how to play");
                m_howToPlay.SetActive(false);
                Time.timeScale = 1;
                return;
        }

        Cursor.visible = false;
    }
}
