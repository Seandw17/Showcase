using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using static CSVWriter;

public class OutroTextEditor : EditorWindow
{
    static List<string> m_outroText;
    Vector2 m_scrollPos = Vector2.zero;

    [MenuItem("Interview Settings/Outro Text")]
    static void Init()
    {
        OutroTextEditor window =
            (OutroTextEditor)GetWindow(typeof(OutroTextEditor));
        EditorStyles.textField.wordWrap = true;
        m_outroText = CSVLoader.LoadOutroText().ToList();

        window.Show();
    }

    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        for (int index = 0; index < m_outroText.Count; index++)
        {
            m_outroText[index] = EditorGUILayout.TextArea(m_outroText[index]);
            if (GUILayout.Button("Delete Line"))
            {
                m_outroText.RemoveAt(index);
            }
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Add Line"))
        {
            m_outroText.Add("");
        }
        if (GUILayout.Button("Save Changes"))
        {
            WriteIntroOutroText("Outro.txt", m_outroText);
        }
    }
}
