﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Author: Alec

public class w_QuestionManager : MonoBehaviour
{
    TextMeshPro m_questionBox;
    List<List<KeyValuePair<e_identifier, s_questionData>>> m_questions;
    ButtonData[] m_buttonPool;
    int m_currentQuestion;
    ConversationStore m_playerConversationStore;
    ButtonData m_button;

    /// <summary>
    /// Time user has to answer a question
    /// </summary>
    [SerializeField] const float m_timeBetweenQuestions = 20.0f;
    float m_currentTime = m_timeBetweenQuestions;
    
    /// <summary>
    /// How many buttons we want to load on start
    /// </summary>
    [SerializeField] const int m_buttonPoolSize = 5;

    /// <summary>
    /// How many questions we should ask in this session
    /// </summary>
    [SerializeField] int m_questionsToAsk = 5;

    // Start is called before the first frame update
    void Start()
    {
        // Loading in button prefab
        m_button = Resources.Load<GameObject>("Prefabs/Button")
            .GetComponent<ButtonData>();

        // acquiring relevant data
        m_questionBox = GetComponent<TextMeshPro>();
        m_questions = new w_CSVLoader().ReadCSV("Test");
        m_playerConversationStore = FindObjectOfType<ConversationStore>();

        // use values to set data
        Vector3 spawnLocation = transform.position;
        // pool our buttons
        m_buttonPool = new ButtonData[m_buttonPoolSize];
        for (int index = 0; index < m_buttonPoolSize; index++)
        {
            spawnLocation = new Vector3(spawnLocation.x,
                spawnLocation.y - 5, spawnLocation.z);
            GameObject temp = Instantiate(m_button.gameObject,
                spawnLocation, transform.rotation);
            m_buttonPool[index] = temp.GetComponent<ButtonData>();
            m_buttonPool[index].Register(this);
            m_buttonPool[index].gameObject.SetActive(false);
        }

        LoadRandomQuestion();
    }

    /// <summary>
    /// Load a random question
    /// </summary>
    void LoadRandomQuestion()
    {
        m_currentQuestion++;
        if (m_currentQuestion > m_questionsToAsk)
        {
            // TODO End the level, or something
            throw new System.Exception("Level end reached, " +
                "but there is nothing here");
        }

        // retrieve data
        int nextQuestion = Random.Range(0, m_questions.Count - 1);
        s_questionData questionToDisplay = new s_questionData();

        // TODO have a way to parse context
        e_identifier context = e_identifier.START;

        // check through our questions, and retrieve appropriate data
        foreach (KeyValuePair<e_identifier, s_questionData> pair in
            m_questions[nextQuestion])
        {
            if (pair.Key == context)
            {
                questionToDisplay = pair.Value;
                break;
            }
        }

        // check we have returned a value
        Debug.Assert(!questionToDisplay.Equals(new s_questionData()),
            "An error has occured finding the quesiton");

        // use values to set data
        m_questionBox.SetText(questionToDisplay.question);
        for (int index = 0; index < questionToDisplay.options.Count; index++)
        {
            if (m_playerConversationStore.CheckHasFlag(
                questionToDisplay.options[index].unlockCriteria))
            {
                m_buttonPool[index].SetValue(questionToDisplay.options[index]);
                m_buttonPool[index].gameObject.SetActive(true);
            }
            else
            {
                // TODO add a lock graphic or something
            }
        }

        Debug.Log("Chose Question: " + questionToDisplay.question);

        // remove our question to prevent repeated valeus
        m_questions.RemoveAt(nextQuestion);

        m_currentTime = m_timeBetweenQuestions;
        StartCoroutine(WaitForAnswer());
    }

    /// <summary>
    /// Process the result of our question, attach this to a button
    /// </summary>
    public void ProcessQuestionResult(s_Questionresponse _chosenResponse)
    {
        StopCoroutine(WaitForAnswer());

        m_playerConversationStore.ProcessAnswer(_chosenResponse,
            m_questionBox.text);

        // Turn of buttons for now
        foreach (ButtonData button in m_buttonPool)
        {
            button.gameObject.SetActive(false);
        }
        m_questionBox.SetText("");

        LoadRandomQuestion();
    }

    /// <summary>
    /// Start a timer that waits for an answer from player
    /// </summary>
    /// <returns> null upon completion </returns>
    IEnumerator WaitForAnswer()
    {
        while (m_currentTime > 0.0f)
        {
            m_currentTime -= Time.deltaTime;
            Debug.Log(m_currentTime);
        }

        m_playerConversationStore.PlayerWasSilent(m_questionBox.text);
        LoadRandomQuestion();

        yield return null;
    }
}