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

public struct s_finalResponse
{
    // TODO implement with feedback from designer
}
