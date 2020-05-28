using System.Collections.Generic;

// Author: Alec

// TODO comply to whatever response system we end up using

/// <summary>
/// Struct to hold a response
/// </summary>
public struct s_Questionresponse
{
    public string response;
    public e_connotes feel;
    public e_unlockFlag unlockCriteria;
}

/// <summary>
/// Struct to hold a <see cref="question"/>
/// </summary>
public struct s_questionData
{
    public string question;
    public List<s_Questionresponse> options;
}

/// <summary>
/// A struct that shows a player response to a question
/// </summary>
public struct s_playerResponse
{
    public string question;
    public s_Questionresponse playerResponse;
}
