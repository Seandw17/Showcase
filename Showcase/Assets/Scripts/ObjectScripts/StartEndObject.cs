using UnityEngine;
using static LevelChange;

/// <summary>
/// Class for a start / end game object
/// </summary>
public class StartEndObject : InteractableObjectBase
{
    /// <summary>
    /// Does this start the game?
    /// </summary>
    [SerializeField] bool m_startButton;

    /// <summary>
    /// Click the button and start / end the game
    /// </summary>
    public override void Interact()
    {
        if (m_startButton) { ChangeLevel("ChooseOutfit"); }
        else { ChangeLevel("TitleScreen"); }
    }
}
