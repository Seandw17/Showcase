using System;
using System.Collections.Generic;
using UnityEngine;

// Author: Alec

// TODO add a way of determening player response 
public class ConversationStore : MonoBehaviour
{
    e_unlockFlag unlockedFlags = e_unlockFlag.NONE;
    List<s_response> m_playerResponses;

    private void Start()
    {
        m_playerResponses = new List<s_response>();
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
    public void PlayerWasSilent()
    {
        // TODO implement
        throw new NotImplementedException();
    }

    /// <summary>
    /// Process a player resposne to a question
    /// </summary>
    /// <param name="_Response"> the response </param>
    public void ProcessAnswer(s_response _response)
    {
        Debug.Assert(!_response.Equals(new s_response()));
        m_playerResponses.Add(_response);
    }

    public s_finalResponse ProcessFinalResult()
    {
        // TODO implement
        throw new NotImplementedException();
    }
}
