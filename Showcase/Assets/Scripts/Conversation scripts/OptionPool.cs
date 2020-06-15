using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ConversationStore;

/// <summary>
/// Class to store options for use in
/// </summary>
public class OptionPool
{
    OptionData[] m_options;

    public OptionPool(int _size, Vector3 _pos, w_QuestionManager _manager)
    {
        m_options = new OptionData[_size];

        OptionData temp = Resources.Load<GameObject>("Prefabs/Option")
            .GetComponentInChildren<OptionData>();
        Debug.Assert(temp, "Option was not loaded correctly");

        OptionData.Register(_manager);

        // loop through and create appropriate amount of options
        Debug.Log("Creating options");
        for (int index = 0; index < _size; index++)
        {
            if (index > 0)
            {
                _pos = new Vector3(_pos.x, _pos.y - 0.25f, _pos.z);
            }
            GameObject hold =
                Object.Instantiate(temp.transform.parent.gameObject);
            hold.transform.position = _pos;
            m_options[index] = hold.GetComponentInChildren<OptionData>();
            Debug.Log("Created option " + (index + 1) + " of " +
                m_options.Length);
        }
        Debug.Log("All options have been created");
    }

    /// <summary>
    /// Set the details of the options
    /// </summary>
    /// <param name="_response">possible user reponses</param>
    /// <param name="_question">possible questions</param>
    public void Set(List<s_Questionresponse> _response, s_questionData
        _question)
    {
        for (int index = 0; index < m_options.Length; index++)
        {
            m_options[index].SetLocked(!CheckHasFlag(
                _response[index].unlockCriteria));
            m_options[index].SetValue(_response[index], _question.tip);
        }
    }

    /// <summary>
    /// Set the details of the options
    /// </summary>
    /// <param name="_questions">questions player can ask</param>
    public void Set(List<s_playerQuestion> _questions)
    {
        for(int index = 0; index < m_options.Length; index++)
        {
            m_options[index].SetLocked(!CheckHasFlag(_questions[index].flag));
            s_Questionresponse temp = new s_Questionresponse
            {
                rating = e_rating.GREAT,
                response = _questions[index].question
            };
            m_options[index].SetValue(temp, e_tipCategories.NOTASKING);
        }
    }

    /// <summary>
    /// Fade out all of the options
    /// </summary>
    public void TurnOffOptions()
    {
        foreach (OptionData option in m_options)
        {
            if (option.enabled)
            {
                option.StartCoroutine(option.setInactive());
            }
        }
    }

    /// <summary>
    /// Clear out all of the options
    /// </summary>
    /// <returns>yield return null</returns>
    public IEnumerator Clear()
    {
        for (int index = 0; index < m_options.Length; index++)
        {
            Object.Destroy(m_options[index].transform.root.gameObject);
            yield return null;
        }
    }
}
