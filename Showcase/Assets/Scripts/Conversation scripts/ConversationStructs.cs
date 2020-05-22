using System.Collections.Generic;

// Author: Alec

/// <summary>
/// Struct to hold a response
/// </summary>
public struct s_response
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
    public List<s_response> options;
}

/// <summary>
/// Struct to hold final response to be passed later
/// </summary>
public struct s_finalResponse
{
    // TODO implement with feedback from designer
}

/// <summary>
/// A struct that shows a player response to a question
/// </summary>
public struct s_playerResponse
{
    public string question;
    public s_response playerResponse;
}
