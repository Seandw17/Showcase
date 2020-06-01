using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ScoreCard : MonoBehaviour
{
    List<s_playerResponse> m_responses;
    int finalScore = 0;

    /// <summary>
    /// How Many the user needs to pass the interview
    /// </summary>
    [SerializeField] int m_amountNeededToPass = 37;

    /// <summary>
    /// GameObject of waiting text
    /// </summary>
    [SerializeField] GameObject m_watitingText;

    ResponseDisplay[] m_ResponseDisplayPool;

    void Awake()
    {
        m_responses = ConversationStore.ReturnFinalChosenResults();

        StartCoroutine(CalculateResult());
    }

    IEnumerator CalculateResult()
    {
        m_ResponseDisplayPool = new ResponseDisplay[m_responses.Count];

        // Instaniate as many reponse displays as we need
        GameObject temp =
            Resources.Load<GameObject>("Prefab/FinalDisplayOption");

        for (int index = 0; index < m_responses.Count; index++)
        {
            m_ResponseDisplayPool[index] =
                Instantiate(temp).GetComponent<ResponseDisplay>();
            m_ResponseDisplayPool[index]
                .SetValue(m_responses[index].question,
                m_responses[index].playerResponse.response,
                m_responses[index].playerResponse.rating);
            yield return null;
        }

        m_watitingText.SetActive(true);

        foreach (s_playerResponse response in m_responses)
        {
            // add to total score
            finalScore += (int)response.playerResponse.rating;

            yield return null;
        }

        m_responses.Clear();
        Destroy(m_watitingText);
        yield return null;
    }
}
