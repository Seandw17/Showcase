using UnityEngine;
using static FadeIn;
using TMPro;

public class PageMoveObject : InteractableObjectBase
{
    public enum e_direction
    {
        LEFT = 180,
        RIGHT = 0,
        NONE
    }

    static ScoreCard m_card;
    e_direction m_directionToMove;
    bool m_interactable = true;
    bool m_endGame;
    GameObject m_mainMenuButton;
    MeshRenderer m_meshRenderer;

    /// <summary>
    /// Function to set static reference for scorecard parent
    /// </summary>
    /// <param name="_card">the cart that will act as a parent</param>
    static public void Register(ScoreCard _card)
    {
        m_card = _card;   
    }

    private void OnEnable()
    {
        SetAlphaToZero(GetComponent<Renderer>().material);
        StartCoroutine(FadeAsset(GetComponent<Renderer>(), 0.5f, true));
    }

    /// <summary>
    /// Set the intial values
    /// </summary>
    /// <param name="_dir"> the direction move </param>
    public void Set(e_direction _dir)
    {
        m_directionToMove = _dir;
        transform.localRotation = Quaternion.Euler(0, 90, (int)_dir);

        m_mainMenuButton = GetComponentInChildren<TextMeshPro>().gameObject;
        m_mainMenuButton.SetActive(false);

        m_meshRenderer = GetComponent<MeshRenderer>();

        if (_dir != e_direction.RIGHT)
        {
            Destroy(m_mainMenuButton);
        }
    }

    public override void Interact()
    {
        if (m_interactable)
        {
            if (!m_endGame)
            {
                Debug.Assert(m_directionToMove.Equals(e_direction.LEFT) ||
                    m_directionToMove.Equals(e_direction.RIGHT),
                    "Please give button a move direction");

                if (m_directionToMove.Equals(e_direction.LEFT))
                {
                    m_card.GoPageLeft();
                }
                else
                {
                    m_card.GoPageRight();
                }
            }
            else
            {
                // go back to the title screen
                Debug.Log("Returning to TitleScreen");
                LevelChange.ChangeLevel("TitleScreen");
            }
            
        }
    }

    public void SetInteractable(bool _value)
    {
        m_interactable = _value;
    }

    /// <summary>
    /// Turn this into a level end button
    /// </summary>
    /// <param name="_state">whether this ends the level or not</param>
    public void ToggleEnd(bool _state)
    {
        Debug.Assert(m_directionToMove == e_direction.RIGHT,
            "Wrong button passed turn off function");
        m_mainMenuButton.SetActive(_state);
        m_endGame = _state;
    }

    public void ToggleVisible(bool _state)
    {
        Debug.Assert(m_directionToMove == e_direction.LEFT,
            "Wrong button was passed this function");
        m_meshRenderer.enabled = _state;
    }
}
