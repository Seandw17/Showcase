using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class w_QuestionManager : MonoBehaviour
{
    TextMeshPro m_questionBox;
    List<List<KeyValuePair<e_identifier, s_questionData>>> m_questions;
    ButtonData[] m_buttonPool;
    int m_currentQuestion;

    /// <summary>
    /// Time user has to answer a question
    /// </summary>
    const float m_timeBetweenQuestions = 20.0f;
    float m_currentTime = m_timeBetweenQuestions;

    /// <summary>
    /// The button we want to use for each options
    /// </summary>
    [SerializeField] ButtonData m_button;

    /// <summary>
    /// How many buttons we want to load on start
    /// </summary>
    [SerializeField] int m_buttonPoolSize = 5;

    /// <summary>
    /// How many questions we should ask in this session
    /// </summary>
    [SerializeField] int m_questionsToAsk = 5;

    // Start is called before the first frame update
    void Start()
    {
        // acquiring relevant data
        m_questionBox = GetComponent<TextMeshPro>();
        m_questions = new w_CSVLoader().ReadCSV("Test");

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
        if (m_currentQuestion > m_questionsToAsk) { /* end */ }

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
            // TODO Query the game manager, if we have the unlock criteria, display
            m_buttonPool[index].SetValue(
                questionToDisplay.options[index].response,
                questionToDisplay.options[index].feel);
            m_buttonPool[index].gameObject.SetActive(true);
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
    public void ProcessQuestionResult(string response, e_connotes feel)
    {
        StopCoroutine(WaitForAnswer());

        // Pass it to a game manager or something

        // Turn of buttons for now
        foreach (ButtonData button in m_buttonPool)
        {
            button.gameObject.SetActive(false);
        }

        LoadRandomQuestion();
    }

    IEnumerator WaitForAnswer()
    {
        while (m_currentTime > 0.0f)
        {
            m_currentTime -= Time.deltaTime;
            Debug.Log(m_currentTime);
        }

        ProcessSilence();

        yield return null;
    }

    void ProcessSilence()
    {
        // TODO pass silent value to gameManager
        LoadRandomQuestion();
    }

    /*
    /// <summary>
    /// Load the next question list
    /// </summary>
    void LoadNextQuestion()
    {
        if (m_currentQuestion != m_questions.Count)
        {
            // acquire the current question
            List<KeyValuePair<string, s_questionData>> currentQuestionVariations =
                m_questions[m_currentQuestion];

            string keyValue = "What Do You Think of This?";
            // TODO PARSE WHAT DETAILS WE WILL NEED

            s_questionData questionToDisplay = new s_questionData();
            foreach (KeyValuePair<string, s_questionData> pair
                in currentQuestionVariations)
            {
                Debug.Log(pair.Key);
                // when we found the relevent pair
                if (pair.Key.Equals(keyValue))
                {
                    questionToDisplay = pair.Value;
                    break;
                }

                // if we have reached the end of the loop without breaking
                throw new System.Exception
                    ("An error has occured finding the question required");
            }

            // use values to set data
            m_questionBox.SetText(questionToDisplay.question);
            for (int index = 0; index < questionToDisplay.options.Count; index++)
            {
                m_buttonPool[index].SetValue(
                    questionToDisplay.options[index].response,
                    questionToDisplay.options[index].feel);
                m_buttonPool[index].gameObject.SetActive(true);
            }

            m_currentQuestion++;
        }

        else
        {
            // End
        }
    }
    */
}
