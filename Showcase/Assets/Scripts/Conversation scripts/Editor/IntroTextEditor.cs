using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class IntroTextWindow : EditorWindow
{
    static List<string> m_introText;
    Vector2 m_scrollPos = Vector2.zero;

    [MenuItem("Interview Settings/Intro Text")]
    static void Init()
    {
         IntroTextWindow window =
            (IntroTextWindow)GetWindow(typeof(IntroTextWindow));

        m_introText = w_CSVLoader.LoadIntroText().ToList();
        EditorStyles.textField.wordWrap = true;
        window.Show();
    }

    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        for (int index = 0; index < m_introText.Count; index++)
        {
            m_introText[index] = EditorGUILayout.TextArea(m_introText[index]);

            if (GUILayout.Button("Delete Line"))
            {
                m_introText.RemoveAt(index);
            }
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Add Line"))
        {
            m_introText.Add("");
        }
        if (GUILayout.Button("Save Changes"))
        {
            WriteBack();
        }
    }

    void WriteBack()
    {
        // TODO write back
    }
}
