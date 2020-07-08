using TMPro;
using UnityEngine;

// Author: Alec

/// <summary>
/// Class for a repsponse diplay box
/// </summary>
public class ResponseDisplay : MonoBehaviour
{
    TextMeshPro m_textBox;

    private void Awake()
    {
        m_textBox = GetComponent<TextMeshPro>();
    }

    /// <summary>
    /// Set Value for text box
    /// </summary>
    /// <param name="_question">the question</param>
    /// <param name="_response">response from user</param>
    /// <param name="score">score given</param>
    public void SetValue(string _question, string _response, e_rating score)
    {
        m_textBox.SetText("For the question: '" + _question + "'" + '\n' +
            "You Said: '" + _response + "'" +'\n'
            + "This is a: " + score.ToString()
            + " response, worth " + ((int) score).ToString() + " points");
    }

    /// <summary>
    /// Set Value for text box
    /// </summary>
    /// <param name="_tip">tip that you wish to display</param>
    public void SetValue(string _tip)
    {
        m_textBox.SetText(_tip);
    }

    public bool IsTextNull()
    {
        return m_textBox.text.Equals("");
    }
}
