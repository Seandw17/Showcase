using System.Collections;
using TMPro;
using UnityEngine;
using static FadeIn;

// Author: Alec

// TODO material swapping

public class OptionData : InteractableObjectBase
{
    TextMeshPro m_textValue;
    static QuestionManager m_questionManager;
    Questionresponse m_responseForThisButton;
    bool m_isInteractible;
    Renderer m_renderer;
    MeshRenderer m_meshRenderer;
    MeshFilter m_meshFilter;
    Coroutine m_fadeText, m_fadeRenderer;
    int m_questionID;
    static InterviewUIPopUp m_popUp;

    [SerializeField] Material m_matInactive, m_matActive;
    [SerializeField] Mesh m_Inactive, m_active;

    private void Awake()
    {
        m_textValue = GetComponent<TextMeshPro>();
        m_renderer = transform.parent.GetComponent<Renderer>();
        m_meshRenderer = transform.parent.GetComponent<MeshRenderer>();
        m_meshRenderer.enabled = false;
        m_meshFilter = transform.parent.GetComponent<MeshFilter>();
        SetAlphaToZero(m_renderer.material);
        SetAlphaToZero(m_textValue);
        SetShouldGlow(false);
    }

    /// <summary>
    /// Function to set the manager
    /// </summary>
    /// <param name="_questionManager"> the manager object </param>
    static public void Register(QuestionManager _questionManager, InterviewUIPopUp _popUp)
    {
        m_questionManager = _questionManager;
        m_popUp = _popUp;
    }

    /// <summary>
    /// Function to set the intial values of the button
    /// </summary>
    /// <param name="_value"> what will be displayed in game</param>
    /// <param name="_connotation"> what feelings should be returned </param>
    public void SetValue(Questionresponse _response, e_tipCategories _tip)
    {
        m_meshRenderer.enabled = true;
        m_textValue.SetText(_response.response);
        m_responseForThisButton = _response;
        m_responseForThisButton.tip = _tip;
        gameObject.SetActive(true);
        if (m_fadeRenderer != null) { StopCoroutine(m_fadeRenderer); }
        if (m_fadeText != null) { StopCoroutine(m_fadeText); }
        if (m_isInteractible)
        {
            SetAlphaToZero(m_textValue);
            SetAlphaToZero(m_renderer.material);
            m_fadeRenderer = StartCoroutine(FadeAsset(m_renderer, 0.5f, true));
            m_fadeText = StartCoroutine(FadeAsset(m_textValue, 0.5f, true));
        }
    }

    /// <summary>
    /// Function to set the initial values of the button
    /// </summary>
    /// <param name="_response">response of given option</param>
    /// <param name="_tip">tip that is displayed on bad</param>
    /// <param name="_ID">id of response</param>
    public void SetValue(Questionresponse _response, e_tipCategories _tip,
        int _ID)
    {
        m_meshRenderer.enabled = true;
        m_textValue.SetText(_response.response);
        m_responseForThisButton = _response;
        m_responseForThisButton.tip = _tip;
        gameObject.SetActive(true);
        if (m_fadeRenderer != null) { StopCoroutine(m_fadeRenderer); }
        if (m_fadeText != null) { StopCoroutine(m_fadeText); }
        if (m_isInteractible)
        {
            SetAlphaToZero(m_renderer.material);
            SetAlphaToZero(m_textValue);
            m_fadeRenderer = StartCoroutine(FadeAsset(m_renderer, 0.5f, true));
            m_fadeText = StartCoroutine(FadeAsset(m_textValue, 0.5f, true));
        }
        m_questionID = _ID;
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
            m_questionManager.ProcessQuestionResult(m_responseForThisButton,
                m_questionID);
            GetObjectOutline().enabled = false;

            // TODO decide
            if ((int)m_responseForThisButton.rating > 1)
            {
                // Good noise
                //FMODUnity.RuntimeManager.PlayOneShot("sound", transform.position);
            }
            else
            {
                // Bad Noise
                //FMODUnity.RuntimeManager.PlayOneShot("sound", transform.position);
            }
            m_popUp.Display("You chose a " +
                m_responseForThisButton.rating +
                " response which earned you " +
                (int)m_responseForThisButton.rating + " points");
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
            m_meshFilter.mesh = m_active;
            m_meshRenderer.material = m_matActive;
        }
        else
        {
            m_meshFilter.mesh = m_Inactive;
            m_meshRenderer.material = m_matInactive;
        }
    }

    public IEnumerator setInactive()
    {
        m_isInteractible = false;
        gameObject.SetActive(true);
        float fadeOutTime = 0.5f;
        GetObjectOutline().enabled = false;
        SetShouldGlow(false);
        if (m_fadeRenderer != null) { StopCoroutine(m_fadeRenderer); }
        if (m_fadeText != null) { StopCoroutine(m_fadeText); }
        SetAlphaToZero(m_renderer.material);
        SetAlphaToZero(m_textValue);

        m_meshFilter.mesh = m_Inactive;
        m_meshRenderer.material = m_matInactive;

        yield return new WaitForSeconds(fadeOutTime + 1);
    }
}
