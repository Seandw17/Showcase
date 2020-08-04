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
        HOWTO,
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
            case action.HOWTO:
                Time.timeScale = 0;
                Debug.Assert(m_howToPlay != null,
                    "Set the gameobject for how to play");
                m_howToPlay.SetActive(true);
                break;
            case action.QUIT:
                Application.Quit();
                break;
            case action.START:
                Time.timeScale = 1;
                ChangeLevel("CharacterSelection");
                break;
        }

        Cursor.visible = false;
    }
}
