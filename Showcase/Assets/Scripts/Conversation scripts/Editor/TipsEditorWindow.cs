using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using static CSVWriter;

public class TipsEditorWindow : EditorWindow
{
    Vector2 m_scrollPos = Vector2.zero;
    static Dictionary<e_tipCategories, string> m_tips;

    [MenuItem("Interview Settings/Tips")]
    static void Init()
    {
        TipsEditorWindow window =
            (TipsEditorWindow)GetWindow(typeof(TipsEditorWindow));

        w_CSVLoader.LoadTips(out m_tips);
        
        window.Show();
    }

    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        for (int index = 2; index < m_tips.Count; index++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label((e_tipCategories) (1 << index) + ":");
            Debug.Log((e_tipCategories) (1 << index));
            m_tips[(e_tipCategories) (1 << index)] = EditorGUILayout.TextArea(m_tips[
                (e_tipCategories)(1 << index)]);
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();

        if (GUILayout.Button("Save Changes"))
        {
            WriteTips(m_tips);
        }
    }
}
