using UnityEngine;
using static FadeIn;

public class PageMoveObject : InteractableObjectBase
{
    public enum e_direction
    {
        LEFT = 90,
        RIGHT = -90,
        NONE
    }

    static ScoreCard m_card;
    e_direction m_directionToMove;
    bool m_interactable = true;

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
        transform.rotation = Quaternion.Euler(0, 0, (int)_dir);
    }

    public override void Interact()
    {
        if (m_interactable)
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
    }

    public void SetInteractable(bool _value)
    {
        m_interactable = _value;
    }
}
