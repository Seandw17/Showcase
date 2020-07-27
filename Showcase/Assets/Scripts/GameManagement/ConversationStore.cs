using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Author: Alec

static public class ConversationStore
{
    static e_unlockFlag m_unlockedFlags = e_unlockFlag.NONE;
    static List<s_playerResponse> m_playerResponses;
    static e_tipCategories m_tips;
    static int m_timesLookedAway;

    static int m_timesArrivedOnTime;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Init() =>
        m_playerResponses = new List<s_playerResponse>();

    /// <summary>
    /// Add a unlock flag to the player
    /// </summary>
    /// <param name="_flag"> the flag to add </param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public void RegisterUnlockFlag(e_unlockFlag _flag) =>
        m_unlockedFlags |= _flag;

    /// <summary>
    /// Return if a flag is present in the player data
    /// </summary>
    /// <param name="_flag"> the flag to check </param>
    /// <returns> returns if has </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public bool CheckHasFlag(e_unlockFlag _flag) =>
        m_unlockedFlags.HasFlag(_flag);

    /// <summary>
    /// Signals that the player was silent
    /// </summary>
    /// <param name="_question"> the question that was active </param>
    static public void PlayerWasSilent(string _question)
    {
        s_playerResponse silentResponse = new s_playerResponse();
        silentResponse.playerResponse = new Questionresponse
        {
            rating = e_rating.AWFUL,
            response = "Stayed Silent"
        };
        silentResponse.question = _question;
        AddTip(e_tipCategories.SILENT);
        m_playerResponses.Add(silentResponse);
    }

    /// <summary>
    /// Processes a given answer
    /// </summary>
    /// <param name="_response"> the reponse the player chose </param>
    /// <param name="_question"> the active question </param>
    static public void ProcessAnswer(Questionresponse _response,
        string _question)
    {
        Debug.Assert(!_response.Equals(new Questionresponse())
            && !_question.Equals(""));

        s_playerResponse temp = new s_playerResponse();
        temp.playerResponse = _response;
        temp.question = _question;

        if (_response.rating.Equals(e_rating.AWFUL) ||
            _response.rating.Equals(e_rating.BAD))
        {
            AddTip(_response.tip);
        }

        m_playerResponses.Add(temp);
    }

    /// <summary>
    /// Returns the final set of chosen results
    /// </summary>
    /// <returns> the list of Player Response structs </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static public List<s_playerResponse> ReturnFinalChosenResults() =>
        m_playerResponses;

    /// <summary>
    /// Add a tip
    /// </summary>
    /// <param name="_tip"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void AddTip(e_tipCategories _tip)
    {
        if (!m_tips.HasFlag(_tip))
        {
            m_tips |= _tip;
        }
    }

    /// <summary>
    /// returns if the only flag is none
    /// </summary>
    /// <returns></returns>
    public static bool IsOnlyNoneFlag() =>
        m_unlockedFlags.Equals(e_unlockFlag.NONE);

    /// <summary>
    /// Return the player tips
    /// </summary>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static e_tipCategories GetPlayerTips() => m_tips;

    /// <summary>
    /// Set that the player was on time to interviewer
    /// </summary>
    public static void DidntReachedInterviewerOnTime()
    {
        if (!m_tips.HasFlag(e_tipCategories.LATEINTERVIEW))
        {
            m_timesArrivedOnTime++;
            AddTip(e_tipCategories.LATEINTERVIEW);
        }
    }

    /// <summary>
    /// Set that the player arrived to waiting area on time
    /// </summary>
    public static void DidntArrivedInWaitingAreaOnTime()
    {
        if (!m_tips.HasFlag(e_tipCategories.LATEWAITING))
        {
            m_timesArrivedOnTime++;
            AddTip(e_tipCategories.LATEWAITING);
        }
    }

    public static void DidntArrivedToShopOnTime()
    {
        if (!m_tips.HasFlag(e_tipCategories.LATESHOP))
        {
            m_timesArrivedOnTime++;
            AddTip(e_tipCategories.LATESHOP);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetDidntArrivedOnTime() => m_timesArrivedOnTime;

    /// <summary>
    /// Call if the player has looked away from the interviewer
    /// </summary>
    public static void LookedAway()
    {
        if (m_timesLookedAway != 5)
        {
            AddTip(e_tipCategories.LOOKEDAWAY);
            m_timesLookedAway++;
            Debug.Log("Registered that player has looked away " +
                m_timesLookedAway + " of 5 times");
        }
        
    }

    /// <summary>
    /// return how many times the player looked away 
    /// </summary>
    /// <returns>how many times, up to 5 the player has looked away</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetLookedAway() => m_timesLookedAway;
}
