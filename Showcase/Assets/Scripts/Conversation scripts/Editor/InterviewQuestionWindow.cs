using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

// Author: Alec

/// <summary>
/// Class to manage the interview question window for the editor
/// </summary>
public class InterviewQuestionWindow : EditorWindow
{
    static List<bool> m_displayQuestionVariations;
    static List<bool> m_displayResponseVariations;
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

        m_questions = CSVLoader.LoadQuestionData("IQuestions");
        m_displayQuestionVariations = new List<bool>();
        m_displayResponseVariations = new List<bool>();

        foreach (QuestionData question in m_questions)
        {
            m_displayQuestionVariations.Add(false);
            m_displayResponseVariations.Add(false);
        }

        window.Show();

    }

    /// <summary>
    /// Display the GUI
    /// </summary>
    private void OnGUI()
    {
        m_scrollPos = GUILayout.BeginScrollView(m_scrollPos, false, true);

        for (int index = 0; index < m_questions.Count; index++)
        {
            GUILayout.Label("Question: " +
            m_questions[index].questions[e_rating.NONE],
            EditorStyles.wordWrappedLabel);

            m_displayQuestionVariations[index] =
                EditorGUILayout.Foldout(m_displayQuestionVariations[index],
                "Question Variations", true);

            if (m_displayQuestionVariations[index])
            {
                QuestionUIDIsplay temp = CreateInstance<QuestionUIDIsplay>();
                temp.Display(m_questions[index].questions);
            }

            m_displayResponseVariations[index] =
                EditorGUILayout.Foldout(m_displayResponseVariations[index],
                "Response Variations", true);
            if (m_displayResponseVariations[index])
            {
                UIResponseVariations.Display(m_questions[index].options);
                if (GUILayout.Button("Add Response"))
                {
                    m_questions[index].options.Add(new Questionresponse
                    {
                        response = "New response",
                        rating = e_rating.OK,
                        unlockCriteria = e_unlockFlag.NONE,
                        tip = e_tipCategories.NOTASKING
                    });
                }

            }

            m_questions[index].tip = (e_tipCategories)EditorGUILayout.
                EnumPopup("Tip to display: ", m_questions[index].tip);

            if (GUILayout.Button("Delete Question"))
            {
                m_questions.RemoveAt(index);
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
            CSVWriter.WriteInterviewerQuestions(m_questions);
        }
    }

    /// <summary>
    /// Redraw on update
    /// </summary>
    public void OnInspectorUpdate() => Repaint();

    /// <summary>
    /// Function to add a blank question
    /// </summary>
    void AddQuestion()
    {
        QuestionData dummy = new QuestionData();

        dummy.ID = m_questions[m_questions.Count - 1].ID + 1;
        dummy.tip = e_tipCategories.NOTASKING;
        dummy.options = new List<Questionresponse>();
        dummy.options.Add(new Questionresponse
        {
            rating = e_rating.OK,
            response = "New Response",
            unlockCriteria = e_unlockFlag.NONE,
            tip = e_tipCategories.NONE
        });
        dummy.questions = new Dictionary<e_rating, string>();
        foreach (e_rating rating in Enum.GetValues(typeof(e_rating)))
        {
            dummy.questions.Add(rating, rating.ToString() + " Version");
        }

        m_questions.Add(dummy);
        m_displayResponseVariations.Add(false);
        m_displayQuestionVariations.Add(false);
    }
}
