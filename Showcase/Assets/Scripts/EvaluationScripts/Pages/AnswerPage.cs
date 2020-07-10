using UnityEngine;
using TMPro;

// Author: Alec

public class AnswerPage : Page
{
    [SerializeField] bool m_forceStart;

    ResponseDisplay[] m_reponses;

    private void Awake()
    {
        m_reponses = GetComponentsInChildren<ResponseDisplay>();
    }

    private void Start()
    {
        // Create test data
        if (m_forceStart)
        {
            s_playerResponse[] testData = new s_playerResponse[3];

            for (int index = 0; index < testData.Length; index++)
            {
                testData[index] = new s_playerResponse
                {
                    question = "What do you think?",
                    playerResponse = new Questionresponse
                    {
                        response = "I like it",
                        rating = e_rating.AWFUL
                    }
                };
            }
            SetValue(testData);
        }
    }

    /// <summary>
    /// Set the values of this page
    /// </summary>
    /// <param name="_reponses"> the reponses to this </param>
    public void SetValue(s_playerResponse[] _reponses)
    {
        Debug.Assert(_reponses.Length < 4, "Too many values passed to " +
            "'set value' of Answer page'");

        for (int index = 0; index < _reponses.Length; index++)
        {
            if (_reponses[index].question == null)
            {
                return;
            }
            else
            {
                m_reponses[index].SetValue(
                    _reponses[index].question,
                    _reponses[index].playerResponse.response,
                    _reponses[index].playerResponse.rating);
            }
        }
    }
}
