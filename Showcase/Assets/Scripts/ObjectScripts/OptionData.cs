using System.Collections;
using TMPro;
using UnityEngine;
using static FadeIn;

// Author: Alec

public class OptionData : InteractableObjectBase
{
    TextMeshPro m_textValue;
    static w_QuestionManager m_questionManager;
    s_Questionresponse m_responseForThisButton;
    bool m_isInteractible;
    Renderer m_renderer;

    private void Awake()
    {
        m_textValue = GetComponent<TextMeshPro>();
        m_renderer = transform.parent.GetComponent<Renderer>();
        SetAlphaToZero(m_renderer.material);
        SetAlphaToZero(m_textValue);
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
    public void SetValue(s_Questionresponse _response, e_tipCategories _tip)
    {
        transform.parent.gameObject.SetActive(true);
        m_textValue.SetText(_response.response);
        m_responseForThisButton = _response;
        m_responseForThisButton.tip = _tip;
        StartCoroutine(FadeAsset(m_renderer, 0.5f, true));
        StartCoroutine(FadeAsset(m_textValue, 0.5f, true));
    }

    /// <summary>
    /// Override of Interact, calls process result on manager
    /// </summary>
    override public void Interact()
    {
        Debug.Log("Option: " + m_textValue.text + "Hit");
        if (m_isInteractible)
        {
            m_questionManager.ProcessQuestionResult(m_responseForThisButton);
        }
    }

    /// <summary>
    /// Set the graphic of the option to locked
    /// </summary>
    /// <param name="_locked">is the object locked</param>
    public void SetLocked(bool _locked)
    {
        m_isInteractible = !_locked;

        // TODO graphical changes
    }

    public IEnumerator setInactive()
    {
        float fadeOutTime = 0.5f;
        StartCoroutine(FadeAsset(m_renderer, fadeOutTime, false));
        yield return new WaitForSeconds(fadeOutTime + 1);
        gameObject.SetActive(false);
    }
}
