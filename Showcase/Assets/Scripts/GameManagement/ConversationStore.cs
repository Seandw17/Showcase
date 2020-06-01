using System.Collections.Generic;
using UnityEngine;

// Author: Alec

public class ConversationStore 
{
    static e_unlockFlag unlockedFlags = e_unlockFlag.NONE;
    static List<s_playerResponse> m_playerResponses;
    static s_playerResponse m_silentResponse;

    ConversationStore()
    {
        m_playerResponses = new List<s_playerResponse>();

        // creating the silent player response
        m_silentResponse = new s_playerResponse();
        m_silentResponse.playerResponse.rating = e_rating.BAD;
        m_silentResponse.playerResponse.response = "Stayed Silent";
    }

    /// <summary>
    /// Add a unlock flag to the player
    /// </summary>
    /// <param name="_flag"> the flag to add </param>
    static public void RegisterUnlockFlag(e_unlockFlag _flag)
    {
        unlockedFlags |= _flag;
    }

    /// <summary>
    /// Return if a flag is present in the player data
    /// </summary>
    /// <param name="_flag"> the flag to check </param>
    /// <returns> returns if has </returns>
    static public bool CheckHasFlag(e_unlockFlag _flag)
    {
        return unlockedFlags.HasFlag(_flag);
    }

    /// <summary>
    /// Signals that the player was silent
    /// </summary>
    /// <param name="_question"> the question that was active </param>
    static public void PlayerWasSilent(string _question)
    {
        m_silentResponse.question = _question;
        m_playerResponses.Add(m_silentResponse);
    }

    /// <summary>
    /// Processes a given answer
    /// </summary>
    /// <param name="_response"> the reponse the player chose </param>
    /// <param name="_question"> the active question </param>
    static public void ProcessAnswer(s_Questionresponse _response,
        string _question)
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
    static public List<s_playerResponse> ReturnFinalChosenResults()
    {
        return m_playerResponses;
    }
}
