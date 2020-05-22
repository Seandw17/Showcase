using System;
using System.Collections.Generic;
using UnityEngine;

// Author: Alec

// TODO add a way of determening player response 
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
        s_response temp = new s_response();
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
    /// Process that the player was silent
    /// </summary>
    public void PlayerWasSilent(string _question)
    {
        m_silentResponse.question = _question;
        m_playerResponses.Add(m_silentResponse);
    }

    /// <summary>
    /// Process a player resposne to a question
    /// </summary>
    /// <param name="_Response"> the response </param>
    public void ProcessAnswer(s_response _response, string _question)
    {
        Debug.Assert(!_response.Equals(new s_response())
            && !_question.Equals(""));

        s_playerResponse temp = new s_playerResponse();
        temp.playerResponse = _response;
        temp.question = _question;

        m_playerResponses.Add(temp);
    }

    public s_finalResponse ProcessFinalResult()
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
