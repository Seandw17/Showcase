using UnityEngine;

// Author: Alec

public class AnswerPage : Page
{
    ResponseDisplay[] m_reponses;

    private void Awake()
    {
        m_reponses = new ResponseDisplay[3];
        Vector3 newPos = Vector3.zero;
        for (int index = 0; index < m_reponses.Length; index++)
        {
            ResponseDisplay temp = Instantiate(Resources.Load<GameObject>
                ("Prefabs/ResponseDisplay")).GetComponent<ResponseDisplay>();
            m_reponses[index] = temp;
            m_reponses[index].transform.SetParent(transform);

            if (index > 0)
            {
                newPos.y -= 0.3f;
                m_reponses[index].transform.position = newPos;
            }
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
            s_playerResponse currentResponse = _reponses[index];
            m_reponses[index].SetValue(
                currentResponse.question,
                currentResponse.playerResponse.response,
                currentResponse.playerResponse.rating);
        }
    }
}
