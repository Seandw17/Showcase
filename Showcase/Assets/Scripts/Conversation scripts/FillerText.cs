using System.Collections.Generic;

/// <summary>
/// Class to manage loading filler text
/// </summary>
public class FillerText
{
    static List<string> m_fillerText;
    static bool m_nextIsSilent;

    // Start is called before the first frame update
    public FillerText()
    {
        m_fillerText = w_CSVLoader.LoadInFillerText();
    }

    /// <summary>
    /// Function to return a random piece of filler text
    /// </summary>
    /// <returns>filler text as string</returns>
    public string ReturnFillerText()
    {
        if (m_nextIsSilent)
        {
            m_nextIsSilent = false;
            return "Nothing to say?... Ok then";
        }
        else
        {
            int index = UnityEngine.Random.Range(0, m_fillerText.Count - 1);
            string returnVal = m_fillerText[index];
            m_fillerText.RemoveAt(index);
            return returnVal;
        }
    }

    static public void Silent()
    {
        m_nextIsSilent = true;
    }
}
