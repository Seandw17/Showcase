using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InterviewUIPopUp : MonoBehaviour
{
    Text m_textBox;
    GameObject m_parent;
    IEnumerator m_displaying;

    // Start is called before the first frame update
    void Start()
    {
        m_textBox = GetComponentInChildren<Text>();

        m_parent = m_textBox.gameObject.transform.parent.gameObject;
        m_parent.SetActive(false);
    }

    /// <summary>
    /// Display the notification text
    /// </summary>
    /// <param name="_text">what text to display</param>
    public void Display(string _text)
    {
        m_parent.SetActive(true);
        m_textBox.text = (_text);

        if (m_displaying != null)
        {
            StopCoroutine(m_displaying);
        }

        m_displaying = DisplayTime();

        StartCoroutine(m_displaying);
    }

    /// <summary>
    /// Count down to text being inactive
    /// </summary>
    /// <returns>1 frame wait</returns>
    IEnumerator DisplayTime()
    {
        float waitTime = 4.0f;

        while (waitTime > 0)
        {
            if (!PauseMenu.IsPaused())
            {
                waitTime -= Time.deltaTime;
            }
            yield return null;
        }

        m_parent.SetActive(false);
    }
}
