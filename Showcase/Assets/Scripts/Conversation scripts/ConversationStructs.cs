using System.Collections.Generic;

// struct to hold an individual response
public struct s_response
{
    public string response;
    public e_connotes feel;
    public e_unlockFlag unlockCriteria;
}

// struct to hold a question
public struct s_questionData
{
    public string question;
    public List<s_response> options;
}
