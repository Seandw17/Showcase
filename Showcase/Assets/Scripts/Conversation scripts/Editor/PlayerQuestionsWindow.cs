using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class PlayerQuestionWindow : EditorWindow
{
    static List<PlayerQuestion> m_questions;
    Vector2 m_scrollPos = Vector2.zero;
    static bool[] m_showQuestions;

    [MenuItem("Interview Settings/Player Questions")]
    static void Init()
    {
        PlayerQuestionWindow window =
            (PlayerQuestionWindow)GetWindow(typeof(PlayerQuestionWindow));

        w_CSVLoader.LoadInPlayerQuestions("PQuestions",out m_questions);
        m_showQuestions = new bool[m_questions.Count];

        window.Show();
    }

    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        for (int index = 0; index < m_questions.Count; index++)
        {
            m_showQuestions[index] =
                EditorGUILayout.Foldout(m_showQuestions[index],
                "Question " + m_questions[index].question, true);

            if (m_showQuestions[index])
            {
                m_questions[index].question =
                    EditorGUILayout.TextField("Question: ",
                    m_questions[index].question);

                m_questions[index].response =
                    EditorGUILayout.TextField("Response: ",
                    m_questions[index].response);

                m_questions[index].flag =
                    (e_unlockFlag)EditorGUILayout.EnumPopup
                ("Unlock Flag: ", m_questions[index].flag);

                if (GUILayout.Button("Delete Question"))
                {
                    DeleteResult(index);
                }
            }
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Save Changes"))
        {
            WriteBack();
        }
    }

    void DeleteResult(int _delete)
    {
        // TODO delete
    }

    void WriteBack()
    {
        // TODO write back
    }
}
