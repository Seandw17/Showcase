using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static FadeIn;
using static w_CSVLoader;
using static ConversationStore;

// Author: Alec

/// <summary>
/// Class to manage the gameloop of the question stage of the interviewer stage
/// </summary>
public class w_QuestionManager : MonoBehaviour
{
    TextMeshPro m_questionBox;

    List<s_questionData> m_questions;
    List<s_playerQuestion> m_questionForJob;

    OptionData[] m_buttonPool;
    OptionData m_option;
    
    bool m_endLevel;

    UnityEvent m_processNextStep;
    UnityEvent m_randomQuestion;

    int m_currentQuestion;

    FillerText m_fillerText;

    e_rating m_previous = e_rating.NONE;

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
        m_questions = LoadQuestionData("IQuestions");
        Debug.Log(m_questions.Count);
        // questions for job / brand
        LoadInPlayerQuestions("PQuestions",
            out m_questionForJob);

        Debug.Assert(m_questions.Count >= m_questionsToAsk,
            "There are not enough questions loaded to meet the desired" +
            " amount to be asked");

        // use values to set data
        Vector3 spawnLocation = new Vector3(
            transform.parent.gameObject.transform.position.x,
            transform.parent.gameObject.transform.position.y - 0.25f,
            transform.parent.gameObject.transform.position.z);
        // pool our buttons
        m_buttonPool = new OptionData[m_buttonPoolSize];
        for (int index = 0; index < m_buttonPoolSize; index++)
        {
            if (index > 0)
            {
                spawnLocation = new Vector3(spawnLocation.x,
                    spawnLocation.y - 0.25f, spawnLocation.z);
            }
            GameObject temp = Instantiate(m_option.transform.parent.gameObject);
            temp.transform.root.gameObject.SetActive(false);
            temp.transform.position = spawnLocation;
            m_buttonPool[index] = temp.GetComponentInChildren<OptionData>();
        }

        // set timerslider
        m_timerSlider.maxValue = m_timeBetweenQuestions;
        m_timerSlider.gameObject.SetActive(false);

        // set up events 
        m_processNextStep = new UnityEvent();
        m_processNextStep.AddListener(ProcessNextStep);
        m_randomQuestion = new UnityEvent();
        m_randomQuestion.AddListener(LoadRandomQuestion);

        m_fillerText = new FillerText();

        SetAlphaToZero(transform.parent.GetComponent<Renderer>().material);
        SetAlphaToZero(m_questionBox);
        StartCoroutine(StartInterview());
    }

    /// <summary>
    /// Load a random question
    /// </summary>
    void LoadRandomQuestion()
    {
        m_currentQuestion++;
        if (m_currentQuestion > m_questionsToAsk)
        {
            m_processNextStep.Invoke();
        }
        else
        {
            // retrieve data
            int nextQuestion = UnityEngine.Random.Range(0, m_questions.Count
                - 1);

            Debug.Log(nextQuestion);
            List<s_Questionresponse> playerResponses =
                m_questions[nextQuestion].options;
            s_questionVariations questionToDisplay =
                m_questions[nextQuestion].questions[(int)m_previous];

            // check we have returned a value
            Debug.Assert(!questionToDisplay.Equals(new s_questionData()),
                "An error has occured finding the quesiton");

            // use values to set data
            m_questionBox.SetText(questionToDisplay.question);
            StartCoroutine(FadeAsset(m_questionBox, 0.5f, true));

            for (int index = 0; index < playerResponses.Count; index++)
            {
                // Set locked graphics, values and active, then begin fade
                m_buttonPool[index].SetLocked(
                    ConversationStore.CheckHasFlag(
                    playerResponses[index].unlockCriteria));
                m_buttonPool[index].SetValue(playerResponses[index],
                    m_questions[nextQuestion].tip);
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
        ProcessAnswer(_chosenResponse,
            m_questionBox.text);
        AddTip(_chosenResponse.tip);
        m_previous = _chosenResponse.rating;
        TurnOffButtons();
        m_processNextStep.Invoke();
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

        TurnOffButtons();
        PlayerWasSilent(m_questionBox.text);
        m_processNextStep.Invoke();
    }

    /// <summary>
    /// Fade Out the buttons, turn off the timer and set the text for the
    /// question box to blank
    /// </summary>
    void TurnOffButtons()
    {
        // Turn of buttons for now
        foreach (OptionData button in m_buttonPool)
        {
            StartCoroutine(button.setInactive());
        }
        //StartCoroutine(FadeAsset(m_questionBox, 0.5f, false));
        m_timerSlider.gameObject.SetActive(false);
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
                .CheckHasFlag(m_questionForJob[index].flag));
            s_Questionresponse temp = new s_Questionresponse
            {
                rating = e_rating.GREAT,
                response = m_questionForJob[index].question
            };
            m_buttonPool[index].SetValue(temp, e_tipCategories.NOTASKING);
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
            StartCoroutine(EndLevel());
        }
        else if (m_currentQuestion > m_questionsToAsk)
        {
            m_questions.Clear();
            AskAboutJob();
        }
        else { StartCoroutine(FillInTime(m_fillerText.ReturnFillerText())); }
    }

    /// <summary>
    /// Function to display some filler text
    /// </summary>
    /// <param name="_fillInText">the filler text</param>
    /// <returns> waits for 2 seconds</returns>
    IEnumerator FillInTime(string _fillInText)
    {
        m_questionBox.SetText(_fillInText);
        yield return new WaitForSeconds(2);
        m_randomQuestion.Invoke();
    }

    /// <summary>
    /// The couroutine to end the level
    /// </summary>
    /// <returns>yield for 2 seconds</returns>
    IEnumerator EndLevel()
    {
        string finalChoice = ConversationStore.ReturnFinalChosenResults()
            [ConversationStore.ReturnFinalChosenResults().Count - 1]
            .playerResponse.response;

        string response = "Nothing? Ok then...";

        foreach(s_playerQuestion question in m_questionForJob)
        {
            if (finalChoice.Equals(question.question))
            {
                response = question.response;
                break;
            }
        }

        //TODO push that player was silent on asking there own question

        m_questionBox.SetText(response);
        yield return new WaitForSeconds(2);
        Instantiate(Resources.Load<GameObject>("Prefabs/ScoreCard"),
                transform.position, transform.rotation);
        Destroy(m_timerSlider.gameObject, 2.0f);
        Destroy(gameObject, 2.0f);
    }

    /// <summary>
    /// coroutine to run through the introduction to the questions
    /// </summary>
    /// <returns>1.5 second wait</returns>
    IEnumerator StartInterview()
    {
        m_questionBox.SetText("Hi! nice to meet you");

        WaitForSeconds waitFor = new WaitForSeconds(1.5f);

        StartCoroutine(FadeAsset(transform.parent.GetComponent<Renderer>(),
           0.5f, true));

        yield return waitFor;

        m_questionBox.SetText("I'm just going to ask you some questions,"
            + " this is just so I can get to know you a bit better");

        yield return waitFor;

        m_questionBox.SetText("So please don't feel nervous or anything!");

        yield return waitFor;

        m_questionBox.SetText("Anyway, to start...");

        yield return waitFor;

        m_processNextStep.Invoke();
    }
}