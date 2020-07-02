using System.Collections;
using TMPro;
using UnityEngine;
using static FadeIn;

// Author: Alec

public class OptionData : InteractableObjectBase
{
    TextMeshPro m_textValue;
    static w_QuestionManager m_questionManager;
    Questionresponse m_responseForThisButton;
    bool m_isInteractible;
    Renderer m_renderer;
    Coroutine m_fadeText, m_fadeRenderer;

    private void Awake()
    {
        m_textValue = GetComponent<TextMeshPro>();
        m_renderer = transform.parent.GetComponent<Renderer>();
        SetAlphaToZero(m_renderer.material);
        SetAlphaToZero(m_textValue);
        SetShouldGlow(false);
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
    public void SetValue(Questionresponse _response, e_tipCategories _tip)
    {
        transform.parent.gameObject.SetActive(true);
        m_textValue.SetText(_response.response);
        m_responseForThisButton = _response;
        m_responseForThisButton.tip = _tip;
        gameObject.SetActive(true);
        if (m_fadeRenderer != null) { StopCoroutine(m_fadeRenderer); }
        if (m_fadeText != null) { StopCoroutine(m_fadeText); }
        m_fadeRenderer = StartCoroutine(FadeAsset(m_renderer, 0.5f, true));
        m_fadeText = StartCoroutine(FadeAsset(m_textValue, 0.5f, true));
        //SetShouldGlow(true);
    }

    /// <summary>
    /// Override of Interact, calls process result on manager
    /// </summary>
    override public void Interact()
    {
        Debug.Log("Option: " + m_textValue.text + "Hit");
        Debug.Log("Interactable = " + m_isInteractible);
        if (m_isInteractible)
        {   
            m_questionManager.ProcessQuestionResult(m_responseForThisButton);
            GetObjectOutline().enabled = false;
        }
    }

    /// <summary>
    /// Set the graphic of the option to locked
    /// </summary>
    /// <param name="_locked">is the object locked</param>
    public void SetLocked(bool _locked)
    {
        m_isInteractible = !_locked;
        if (!_locked)
        {
            SetShouldGlow(true);
        }

        // TODO graphical changes
    }

    public IEnumerator setInactive()
    {
        m_isInteractible = false;
        gameObject.SetActive(true);
        float fadeOutTime = 0.5f;
        SetShouldGlow(false);
        if(m_fadeRenderer != null) { StopCoroutine(m_fadeRenderer); }
        if (m_fadeText != null) { StopCoroutine(m_fadeText); }
        StartCoroutine(FadeAsset(m_renderer, fadeOutTime, false));
        StartCoroutine(FadeAsset(m_textValue, fadeOutTime, false));
        yield return new WaitForSeconds(fadeOutTime + 1);
    }
}
