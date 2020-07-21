using TMPro;
using UnityEngine;
using static FadeIn;

// Author: Alec

public class FinalResult : Page
{
    /// <summary>
    /// The TMP object of where the final score will go
    /// </summary>
    [SerializeField] TextMeshPro m_finalScore;

    /// <summary>
    /// The outcome of the players score
    /// </summary>
    [SerializeField] TextMeshPro m_outcomeText;

    /// <summary>
    /// How Many the user needs to pass the interview
    /// </summary>
    [SerializeField] int m_amountNeededToPassPercent = 75;

    /// <summary>
    /// Text for final percent
    /// </summary>
    [SerializeField] TextMeshPro m_finalPercentText;

    /// <summary>
    /// Text that will displayed when pass occurs
    /// </summary>
    [SerializeField] string m_passText;

    /// <summary>
    /// Text that will be displayed when fail occurs
    /// </summary>
    [SerializeField] string m_failText;

    /// <summary>
    /// set the value of the page
    /// </summary>
    /// <param name="_score">the players score</param>
    public void SetValue(int _score, int questionsAsked)
    {
        float amountNeededToPass = (questionsAsked * 5) * 0.75f;
        //m_finalPercentText.SetText(amountNeededToPass + "%");

        m_finalScore.SetText(_score.ToString());

        if (_score >= amountNeededToPass) { m_outcomeText.SetText(m_passText); }
        else { m_outcomeText.SetText(m_failText); }
    }
}
