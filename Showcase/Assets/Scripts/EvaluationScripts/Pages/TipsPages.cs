using UnityEngine;
using TMPro;

/// <summary>
/// Class to generate a tips page
/// </summary>
public class TipsPages : Page
{
    ResponseDisplay[] m_responseDisplays;

    /// <summary>
    /// If we want to test a dummy page
    /// </summary>
    [SerializeField] bool m_forceStart;

    private void Awake()
    { 
        m_responseDisplays = GetComponentsInChildren<ResponseDisplay>();

        // create test data
        if (m_forceStart)
        {
            string[] testData = { "You should answer all questions!",
                "Reallllllyyyy loooonnnngggg ttiiiiiiipppppppp",
                "Emphasise your best features!" };

            SetValue(testData);
        }
    }

    public void SetValue(string[] _tips)
    {
        Debug.Assert(_tips.Length == m_responseDisplays.Length,
            "Too many text " +
            "values have been passed to this functions");

        for (int index = 0; index < m_responseDisplays.Length; index++)
        {
            m_responseDisplays[index].SetValue(_tips[index]);
        }
    }
}
