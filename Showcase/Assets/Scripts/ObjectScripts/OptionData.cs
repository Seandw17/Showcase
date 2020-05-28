using TMPro;
using UnityEngine;

// Author: Alec

public class OptionData : InteractableObjectBase
{
    TextMeshPro m_textValue;
    static w_QuestionManager m_questionManager;
    s_Questionresponse m_responseForThisButton;
    bool m_isInteractible;

    private void Start()
    {
        base.Start();
    }

    private void Awake()
    {
        m_textValue = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// Function to set the manager
    /// </summary>
    /// <param name="_questionManager"> the manager object </param>
    static public void Register(w_QuestionManager _questionManager)
    {
        m_questionManager = _questionManager;
    }

    /// <summary>
    /// Function to set the intial values of the button
    /// </summary>
    /// <param name="_value"> what will be displayed in game</param>
    /// <param name="_connotation"> what feelings should be returned </param>
    public void SetValue(s_Questionresponse _response)
    {
        m_textValue.SetText(_response.response);
        m_responseForThisButton = _response;
    }

    /// <summary>
    /// Override of Interact, calls process result on manager
    /// </summary>
    override public void Interact()
    {
        Debug.Log("Option Hit");
        if (m_isInteractible)
        {
            m_questionManager.ProcessQuestionResult(m_responseForThisButton);
            Debug.Log("Chose " + gameObject.name);
        }
    }

    /// <summary>
    /// Function to return the text of this object
    /// </summary>
    /// <returns> the text mesh pro object </returns>
    public TextMeshPro ReturnText() { return m_textValue; }

    /// <summary>
    /// Set the graphic of the option to locked
    /// </summary>
    /// <param name="_locked">is the object locked</param>
    public void SetLocked(bool _locked)
    {
        m_isInteractible = !_locked;

        // TODO graphical changes
    }
}
