using TMPro;
using UnityEngine;

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

    bool? m_passed;

    /// <summary>
    /// set the value of the page
    /// </summary>
    /// <param name="_score">the players score</param>
    public void SetValue(int _score, int questionsAsked)
    {
        m_finalScore.SetText(_score.ToString());

        if (CalculatePass(CalculatePassingGrade(questionsAsked, 0.6f), _score))
        {
            m_outcomeText.SetText(m_passText);
            m_passed = true;
        }
        else
        {
            m_outcomeText.SetText(m_failText);
            m_passed = false;
        }
    }

    private void OnEnable()
    {
        // if they've passed or failed, play sound
        if (m_passed != null)
        {
            if (m_passed == true)
            {
                //pass
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/pass_grade",
                    GetComponent<Transform>().position);
                Debug.Log("Pass noise");
            }
            else
            {
                // fail
                FMODUnity.RuntimeManager.PlayOneShot("event:/UI/fail_grade",
                    GetComponent<Transform>().position);
                Debug.Log("fail noise");

            }
        }
    }

    /// <summary>
    /// Calculate what is needed to pass based upon the question amount
    /// </summary>
    /// <param name="_questionAmount">how many questions were asked</param>
    /// <param name="_passingPercent">what % is the pass amount</param>
    /// <returns>what score is needed to pass</returns>
    static public float CalculatePassingGrade(int _questionAmount,
        float _passingPercent)
    {
        return Mathf.Round(((_questionAmount - 1) * 5) * _passingPercent);
    }

    /// <summary>
    /// Find out if passed or not
    /// </summary>
    /// <param name="_amountToPass">what score is needed to pass</param>
    /// <param name="_score">the score the player got</param>
    /// <returns>if passed or not</returns>
    static public bool CalculatePass(float _amountToPass, float _score)
    {
        return _score >= _amountToPass;
    }
}
