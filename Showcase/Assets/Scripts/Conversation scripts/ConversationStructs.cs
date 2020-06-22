// Author: Alec

/// <summary>
/// A struct that shows a player response to a question
/// </summary>
public struct s_playerResponse
{
    public string question;
    public Questionresponse playerResponse;
}

/// <summary>
/// Struct to contain player question
/// </summary>
public struct s_playerQuestion
{
    public string question;
    public string response;
    public e_unlockFlag flag;
}
