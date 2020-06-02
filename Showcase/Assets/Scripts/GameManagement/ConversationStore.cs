using System.Collections.Generic;
using UnityEngine;

// Author: Alec

static public class ConversationStore 
{
    static e_unlockFlag unlockedFlags = e_unlockFlag.NONE;
    static List<s_playerResponse> m_playerResponses;

    public static void Init()
    {
        m_playerResponses = new List<s_playerResponse>();
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
        s_playerResponse silentResponse = new s_playerResponse();
        silentResponse.playerResponse.rating = e_rating.BAD;
        silentResponse.playerResponse.response = "Stayed Silent";
        silentResponse.question = _question;
        m_playerResponses.Add(silentResponse);
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

    // TESTING FUNCTION, TO REMOVE
    static public List<s_playerResponse> ReturnTestData()
    {
        PlayerWasSilent("Test 1");
        PlayerWasSilent("Test 2");
        PlayerWasSilent("Test 3");
        PlayerWasSilent("Test 4");
        PlayerWasSilent("Test 5");
        PlayerWasSilent("Test 6");

        return m_playerResponses;
    }
}
