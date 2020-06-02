using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Author: Alec

public class w_QuestionManager : MonoBehaviour
{
    TextMeshPro m_questionBox;
    List<s_questionData> m_questions;
    OptionData[] m_buttonPool;
    int m_currentQuestion;
    OptionData m_option;
    List<KeyValuePair<e_unlockFlag, string>> m_questionForJob;
    bool m_endLevel;

    UnityEvent m_processNextStep;

    /// <summary>
    /// The timer visualisitation
    /// </summary>
    [SerializeField] Slider m_timerSlider;

    /// <summary>
    /// Time user has to answer a question
    /// </summary>
    [SerializeField] float m_timeBetweenQuestions = 20.0f;

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
        // Loading in button prefab
        m_option = Resources.Load<GameObject>("Prefabs/Option")
            .GetComponentInChildren<OptionData>();
        Debug.Assert(m_option, "Option was not loaded correctly");

        OptionData.Register(this);

        // acquiring relevant data
        //questions for player
        m_questionBox = GetComponent<TextMeshPro>();
        m_questions = w_CSVLoader.LoadQuestionData("IQuestions");
        Debug.Log(m_questions.Count);
        // questions for job / brand
        m_questionForJob = w_CSVLoader.LoadInPlayerQuestions("PQuestions");

        Debug.Assert(m_questions.Count >= m_questionsToAsk,
            "There are not enough questions loaded to meet the desired" +
            " amount to be asked");

        // use values to set data
        Vector3 spawnLocation = new Vector3(
            transform.parent.gameObject.transform.position.x,
            transform.parent.gameObject.transform.position.y - 1.5f,
            transform.parent.gameObject.transform.position.z);
        // pool our buttons
        m_buttonPool = new OptionData[m_buttonPoolSize];
        for (int index = 0; index < m_buttonPoolSize; index++)
        {
            if (index > 0)
            {
                spawnLocation = new Vector3(spawnLocation.x,
                    spawnLocation.y - 1f, spawnLocation.z);
            }
            GameObject temp = Instantiate(m_option.transform.parent.gameObject,
                spawnLocation, transform.rotation);
            m_buttonPool[index] = temp.GetComponentInChildren<OptionData>();
            m_buttonPool[index].transform.parent.gameObject.SetActive(false);
        }

        m_timerSlider.maxValue = m_timeBetweenQuestions;
        m_timerSlider.gameObject.SetActive(false);

        m_processNextStep = new UnityEvent();
        m_processNextStep.AddListener(ProcessNextStep);

        ProcessNextStep();
    }

    /// <summary>
    /// Load a random question
    /// </summary>
    void LoadRandomQuestion()
    {
        m_currentQuestion++;
        if (m_currentQuestion > m_questionsToAsk)
        {
            ProcessNextStep();
        }
        else
        {
            // retrieve data
            int nextQuestion = UnityEngine.Random.Range(0, m_questions.Count - 1);

            // TODO have a way to parse context
            e_identifier context = e_identifier.START;

            Debug.Log(nextQuestion);
            List<s_Questionresponse> playerResponses =
                m_questions[nextQuestion].options;
            s_questionVariations questionToDisplay =
                m_questions[nextQuestion].questions[(int)context];

            // check we have returned a value
            Debug.Assert(!questionToDisplay.Equals(new s_questionData()),
                "An error has occured finding the quesiton");

            // use values to set data
            m_questionBox.SetText(questionToDisplay.question);

            Debug.Log(playerResponses.Count);
            for (int index = 0; index < playerResponses.Count; index++)
            {
                Debug.Log(index);
                // Set locked graphics, values and active, then begin fade
                m_buttonPool[index].SetLocked(
                    ConversationStore.CheckHasFlag(
                    playerResponses[index].unlockCriteria));
                m_buttonPool[index].SetValue(playerResponses[index]);
                m_buttonPool[index].transform.parent.gameObject.SetActive(true);
            }

            Debug.Log("Chose Question: " + questionToDisplay.question);

            // remove our question to prevent repeated valeus
            m_questions.RemoveAt(nextQuestion);

            StartCoroutine(WaitForAnswer());
        }
    }

    /// <summary>
    /// Process the result of our question, attach this to a button
    /// </summary>
    public void ProcessQuestionResult(s_Questionresponse _chosenResponse)
    {
        StopCoroutine(WaitForAnswer());
        m_timerSlider.gameObject.SetActive(false);

        ConversationStore.ProcessAnswer(_chosenResponse,
            m_questionBox.text);

        // Turn of buttons for now
        foreach (OptionData button in m_buttonPool)
        {
            button.gameObject.SetActive(false);
        }
        m_questionBox.SetText("");

        ProcessNextStep();
    }

    /// <summary>
    /// Start a timer that waits for an answer from player
    /// </summary>
    /// <returns> null upon completion </returns>
    IEnumerator WaitForAnswer()
    {
        float currentTime = m_timeBetweenQuestions;
        m_timerSlider.gameObject.SetActive(true);

        while (currentTime > 0.0f)
        {
            currentTime -= Time.deltaTime;
            m_timerSlider.value = currentTime;
            yield return null;
        }

        m_timerSlider.gameObject.SetActive(false);
        ConversationStore.PlayerWasSilent(m_questionBox.text);
        m_processNextStep.Invoke();
    }

    /// <summary>
    /// Start question for user
    /// </summary>
    void AskAboutJob()
    {
        m_questionBox.SetText("So do you have any questions about the job" +
            "itself?");

        for (int index = 0; index < m_questionForJob.Count; index++)
        {
            m_buttonPool[index].SetLocked(ConversationStore
                .CheckHasFlag(m_questionForJob[index].Key));
            s_Questionresponse temp = new s_Questionresponse();
            temp.rating = e_rating.GREAT;
            temp.response = m_questionForJob[index].Value;
            m_buttonPool[index].SetValue(temp);
            m_buttonPool[index].gameObject.SetActive(true);
        }

        m_endLevel = true;
        m_questionForJob.Clear();

        StartCoroutine(WaitForAnswer());
    }

    /// <summary>
    /// Process whatever we do next
    /// </summary>
    void ProcessNextStep()
    {
        if (m_endLevel)
        {
            // TODO End the level
            Instantiate(Resources.Load<GameObject>("Prefabs/ScoreCard"),
                transform.position, transform.rotation);
            Destroy(m_timerSlider.gameObject, 2.0f);
            Destroy(gameObject, 2.0f);
        }
        else if (m_currentQuestion > m_questionsToAsk)
        {
            m_questions.Clear();
            AskAboutJob();
        }
        else { LoadRandomQuestion(); }
    }
}