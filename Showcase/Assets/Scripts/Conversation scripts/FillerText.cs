using System.Collections.Generic;

public class FillerText
{
    static List<string> m_fillerText;

    // Start is called before the first frame update
    public void Init()
    {
        m_fillerText = w_CSVLoader.LoadInFillerText();
    }

    public string ReturnFillerText()
    {
        Init();
        int index = UnityEngine.Random.Range(0, m_fillerText.Count - 1);
        string returnVal = m_fillerText[index];
        m_fillerText.RemoveAt(index);
        return returnVal;
    }
}
