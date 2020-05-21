using UnityEngine;
using TMPro;

// Author: Alec

public class ButtonData : InteractableObjectBase
{
    /// <summary>
    /// The Text Mesh Pro of this prefab
    /// </summary>
    [SerializeField] TextMeshPro m_textValue = null;
    w_QuestionManager m_questionManager;
    e_connotes m_connotation;

    /// <summary>
    /// Function to set the manager
    /// </summary>
    /// <param name="_questionManager"> the manager object </param>
    public void Register(w_QuestionManager _questionManager)
    {
        m_questionManager = _questionManager;
    }

    /// <summary>
    /// Function to set the intial values of the button
    /// </summary>
    /// <param name="_value"> what will be displayed in game</param>
    /// <param name="_connotation"> what feelings should be returned </param>
    public void SetValue(string _value, e_connotes _connotation)
    {
        m_textValue.SetText(_value);
        m_connotation = _connotation;
    }

    override public void Interact() 
    {
        m_questionManager.ProcessQuestionResult(m_textValue.text,
            m_connotation);
    }
}
