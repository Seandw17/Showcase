using System.Collections.Generic;
using UnityEngine;

// Author: Alec

public class ConversationStore : MonoBehaviour
{
    e_unlockFlag unlockedFlags = e_unlockFlag.NONE;
    List<s_playerResponse> m_playerResponses;
    s_playerResponse m_silentResponse;

    /// <summary>
    /// Constructor for this object
    /// </summary>
    ConversationStore()
    {
        m_playerResponses = new List<s_playerResponse>();

        m_silentResponse = new s_playerResponse();
        s_Questionresponse temp = new s_Questionresponse();
        temp.response = "Silent";
        temp.feel = e_connotes.NERVOUSNESS; // TODO change this with feedback
        m_silentResponse.playerResponse = temp;
    }

    /// <summary>
    /// Add a unlock flag to the player
    /// </summary>
    /// <param name="_flag"> the flag to add </param>
    public void RegisterUnlockFlag(e_unlockFlag _flag)
    {
        unlockedFlags |= _flag;
    }

    /// <summary>
    /// Return if a flag is present in the player data
    /// </summary>
    /// <param name="_flag"> the flag to check </param>
    /// <returns> returns if has </returns>
    public bool CheckHasFlag(e_unlockFlag _flag)
    {
        return unlockedFlags.HasFlag(_flag);
    }

    /// <summary>
    /// Signals that the player was silent
    /// </summary>
    /// <param name="_question"> the question that was active </param>
    public void PlayerWasSilent(string _question)
    {
        m_silentResponse.question = _question;
        m_playerResponses.Add(m_silentResponse);
    }

    /// <summary>
    /// Processes a given answer
    /// </summary>
    /// <param name="_response"> the reponse the player chose </param>
    /// <param name="_question"> the active question </param>
    public void ProcessAnswer(s_Questionresponse _response, string _question)
    {
        Debug.Assert(!_response.Equals(new s_Questionresponse())
            && !_question.Equals(""));

        s_playerResponse temp = new s_playerResponse();
        temp.playerResponse = _response;
        temp.question = _question;

        m_playerResponses.Add(temp);
    }

    /// <summary>
    /// Returns the final set of chosen results
    /// </summary>
    /// <returns> the list of Player Response structs </returns>
    public List<s_playerResponse> ReturnFinalChosenResults()
    {
        return m_playerResponses;
    }
}
