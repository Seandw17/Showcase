using UnityEngine;

public class AnswerPage : Page
{
    ResponseDisplay[] m_reponses;

    protected override void Init()
    {
        m_reponses = new ResponseDisplay[5];
        for (int index = 0; index < m_reponses.Length; index++)
        {
            ResponseDisplay temp = Instantiate(Resources.Load<GameObject>
                ("Prefabs/ResponseDisplay")).GetComponent<ResponseDisplay>();
            m_reponses[index] = temp;
            m_reponses[index].transform.SetParent(transform);
            // TODO set position
        }
    }

    /// <summary>
    /// Set the values of this page
    /// </summary>
    /// <param name="_reponses"> the reponses to this </param>
    public void SetValue(s_playerResponse[] _reponses)
    {
        Init();

        Debug.Assert(_reponses.Length < 6, "Too many values passed to " +
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
