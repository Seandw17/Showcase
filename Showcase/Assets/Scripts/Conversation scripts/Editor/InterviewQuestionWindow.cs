using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

// Author: Alec

/// <summary>
/// Class to manage the interview question window for the editor
/// </summary>
public class InterviewQuestionWindow : EditorWindow
{
    static bool[] displayQuestionVariations;
    static bool[] displayResponseVariations;
    static List<QuestionData> m_questions;
    Vector2 m_scrollPos = Vector2.zero;

    /// <summary>
    /// Display the window
    /// </summary>
    [MenuItem("Interview Settings/Interviewer Questions")]
    public static void Init()
    {
        InterviewQuestionWindow window =
            (InterviewQuestionWindow)GetWindow(typeof(InterviewQuestionWindow));

        m_questions = w_CSVLoader.LoadQuestionData("IQuestions");
        displayQuestionVariations = new bool[m_questions.Count];
        displayResponseVariations = new bool[m_questions.Count];
        window.Show();

    }

    /// <summary>
    /// Display the GUI
    /// </summary>
    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        // TODO add more options
        for (int index = 0; index < m_questions.Count; index++)
        {
            GUILayout.Label("Question: " +
            m_questions[index].questions[e_rating.NONE],
            EditorStyles.wordWrappedLabel);

            displayQuestionVariations[index] =
                EditorGUILayout.Foldout(displayQuestionVariations[index],
                "Question Variations", true);

            if (displayQuestionVariations[index])
            {
                QuestionUIDIsplay temp = CreateInstance<QuestionUIDIsplay>();
                temp.Display(m_questions[index].questions);
            }

            displayResponseVariations[index] =
                EditorGUILayout.Foldout(displayResponseVariations[index],
                "Response Variations", true);
            if (displayResponseVariations[index])
            {
                UIResponseVariations.Display(m_questions[index].options);
                if (GUILayout.Button("Add Response"))
                {
                    AddResponse();
                }
            }
            
            m_questions[index].tip = (e_tipCategories)EditorGUILayout.
                EnumPopup("Tip to display: ", m_questions[index].tip);

            if (GUILayout.Button("Delete Question"))
            {
                DeleteQuestion(index);
            }
        }

        GUILayout.EndScrollView();

        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Question"))
        {
            AddQuestion();
        }

        if (GUILayout.Button("Save Changes"))
        {
            WriteBack();
        }
    }

    void WriteBack()
    {
        // TODO write back to file
    }

    public void OnInspectorUpdate() => Repaint();

    void AddQuestion()
    {
        // TODO add a question
    }

    void AddResponse()
    {
        //TODO add new response
    }

    void DeleteQuestion(int _toDelete)
    {
        // TODO delete question
    }
}
