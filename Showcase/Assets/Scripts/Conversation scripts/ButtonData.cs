using TMPro;

// Author: Alec

public class ButtonData : InteractableObjectBase
{
    TextMeshPro m_textValue;
    w_QuestionManager m_questionManager;
    s_Questionresponse m_responseForThisButton;

    private void Start()
    {
        m_textValue = GetComponent<TextMeshPro>();
    }

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
        m_questionManager.ProcessQuestionResult(m_responseForThisButton);
    }

    /// <summary>
    /// Function to return the text of this object
    /// </summary>
    /// <returns> the text mesh pro object </returns>
    public TextMeshPro ReturnText() { return m_textValue; }
}
