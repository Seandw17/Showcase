using UnityEngine;
using UnityEditor;

public class OutroTextEditor : EditorWindow
{
    static string[] m_outroText;
    Vector2 m_scrollPos = Vector2.zero;

    [MenuItem("Interview Settings/Outro Text")]
    static void Init()
    {
        OutroTextEditor window =
            (OutroTextEditor)GetWindow(typeof(OutroTextEditor));

        m_outroText = w_CSVLoader.LoadOutroText();

        window.Show();
    }

    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        foreach (string line in m_outroText)
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
