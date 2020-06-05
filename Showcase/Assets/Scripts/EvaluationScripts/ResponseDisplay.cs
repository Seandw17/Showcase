using TMPro;
using UnityEngine;

// Author: Alec

public class ResponseDisplay : MonoBehaviour
{
    public void SetValue(string _question, string _response, e_rating score)
    {
        TextMeshPro textBox = GetComponent<TextMeshPro>();
        textBox.SetText("For the question: '" + _question + "'" + '\n' +
            "You Said: '" + _response + "'" +'\n'
            + "This is a: " + score.ToString()
            + " reponse, worth " + ((int) score).ToString() + " points");
    }
}
