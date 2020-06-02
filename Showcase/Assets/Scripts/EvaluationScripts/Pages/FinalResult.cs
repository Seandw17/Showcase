using TMPro;
using UnityEngine;

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
    [SerializeField] int m_amountNeededToPass = 37;

    protected override void Init()
    {
        throw new System.NotImplementedException();
    }

    /// <summary>
    /// set the value of the page
    /// </summary>
    /// <param name="_score">the players score</param>
    public void SetValue(int _score)
    {
        m_finalScore.SetText(_score.ToString());

        if (_score >= m_amountNeededToPass)
        {
            m_outcomeText.SetText("Congratulations you've passed!");
        }
        else
        {
            m_outcomeText.SetText("I'm sorry you've failed");
        }
    }
}
