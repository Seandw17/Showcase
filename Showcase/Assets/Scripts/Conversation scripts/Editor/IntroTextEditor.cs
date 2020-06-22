using UnityEngine;
using UnityEditor;

public class IntroTextWindow : EditorWindow
{
    static string[] m_introText;
    Vector2 m_scrollPos = Vector2.zero;

    [MenuItem("Interview Settings/Intro Text")]
    static void Init()
    {
         IntroTextWindow window =
            (IntroTextWindow)GetWindow(typeof(IntroTextWindow));

        m_introText = w_CSVLoader.LoadIntroText();

        window.Show();
    }

    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        foreach (string line in m_introText)
        {
            EditorGUILayout.TextArea(line);
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Add Line"))
        {
            AddLine();
        }
        if (GUILayout.Button("Save Changes"))
        {
            WriteBack();
        }
    }

    void AddLine()
    {
        // TODO add a line
    }

    void WriteBack()
    {
        // TODO write back
    }
}
