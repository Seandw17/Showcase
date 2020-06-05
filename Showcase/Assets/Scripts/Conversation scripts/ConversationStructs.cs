using System.Collections.Generic;

// Author: Alec

/// <summary>
/// Struct to hold a response
/// </summary>
public struct s_Questionresponse
{
    public string response;
    public e_rating rating;
    public e_unlockFlag unlockCriteria;
}

/// <summary>
/// Struct to hold a <see cref="question"/>
/// </summary>
public struct s_questionData
{
    public List<s_questionVariations> questions;
    public List<s_Questionresponse> options;
}

public struct s_questionVariations
{
    public e_rating identifier;
    public string question;
}

/// <summary>
/// A struct that shows a player response to a question
/// </summary>
public struct s_playerResponse
{
    public string question;
    public s_Questionresponse playerResponse;
}

public struct s_playerQuestion
{
    public string question;
    public string response;
    public e_unlockFlag flag;
}
