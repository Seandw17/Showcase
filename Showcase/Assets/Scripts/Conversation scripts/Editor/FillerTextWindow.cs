using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using static CSVWriter;

/// <summary>
/// Class to manage the filler text editor window
/// </summary>
public class FillerTextWindow : EditorWindow
{
    static List<string> m_fillerText;
    Vector2 m_scrollPos = Vector2.zero;

    /// <summary>
    /// Function to start
    /// </summary>
    [MenuItem("Interview Settings/Filler Text")]
    public static void Init()
    {
        FillerTextWindow window =
            (FillerTextWindow)GetWindow(typeof(FillerTextWindow));

        m_fillerText = w_CSVLoader.LoadInFillerText();
        EditorStyles.textField.wordWrap = true;
        window.Show();
    }

    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        for (int index = 0; index < m_fillerText.Count; index++)
        {
            m_fillerText[index] =
            EditorGUILayout.TextArea(m_fillerText[index]);
            if (GUILayout.Button("Delete Filler"))
            {
                m_fillerText.RemoveAt(index);
            }
        }

        GUILayout.EndScrollView();

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Filler Text"))
        {
            m_fillerText.Add("");
        }

        if (GUILayout.Button("Save Filler Text"))
        {
            WriteFillerText(m_fillerText);
        }
    }
}

