using TMPro;
using UnityEngine;

public class ResponseDisplay : MonoBehaviour
{
    TextMeshProUGUI textBox;

    void Start()
    {
        textBox = GetComponent<TextMeshProUGUI>();
    }

    public void SetValue(string _question, string _response, e_rating score)
    {
        textBox.SetText(_question + '\n' + "You Said: " + _response + '\n'
            + "This is a: " + score.ToString()
            + " reponse, worth " + ((int) score).ToString() + " points");
    }
}
