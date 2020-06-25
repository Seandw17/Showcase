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

    List<QuestionData> m_questions;
    List<PlayerQuestion> m_questionForJob;

    bool m_endLevel;

    UnityEvent m_processNextStep;
    UnityEvent m_randomQuestion;

    int m_currentQuestion;

    FillerText m_fillerText;

    e_rating m_previous = e_rating.NONE;

    OptionPool m_optionPool;

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

    /// <summary>
    /// how fast the text should fade in
    /// </summary>
    [SerializeField] float m_fadeInSpeed = 0.75f;

    /// <summary>
    /// Interviewer for this level
    /// </summary>
    [SerializeField] InterviewerFace m_interviewer;

    Coroutine m_waitForAnswer, m_fadeText;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(m_interviewer != null, "No reference to interviewer " +
            "exists");

        // acquiring relevant data
        //questions for player
        m_questionBox = GetComponent<TextMeshPro>();
        m_questions = LoadQuestionData("IQuestions");
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

        m_optionPool = new OptionPool(m_buttonPoolSize, spawnLocation, this);

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

        //enabled = false;
        // TODO once we move to unifying the views, remove this
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
            int nextQuestion = Random.Range(0, m_questions.Count
                - 1);

            List<Questionresponse> playerResponses =
                m_questions[nextQuestion].options;
            string questionToDisplay =
                m_questions[nextQuestion].questions[m_previous];

            // check we have returned a value
            Debug.Assert(!questionToDisplay.Equals(new QuestionData()),
                "An error has occured finding the quesiton");

            // use values to set data
            m_questionBox.SetText(questionToDisplay);
            m_fadeText = StartCoroutine(FadeAsset(m_questionBox,
                m_fadeInSpeed, true));

            m_optionPool.Set(playerResponses, m_questions[nextQuestion]);

            Debug.Log("Chose Question: " + questionToDisplay);

            // remove our question to prevent repeated valeus
            m_questions.RemoveAt(nextQuestion);

            m_waitForAnswer = StartCoroutine(WaitForAnswer());
        }
    }

    /// <summary>
    /// Process the result of our question, attach this to a button
    /// </summary>
    public void ProcessQuestionResult(Questionresponse _chosenResponse)
    {
        StopCoroutine(m_waitForAnswer);
        m_timerSlider.gameObject.SetActive(false);
        ProcessAnswer(_chosenResponse,
            m_questionBox.text);
        AddTip(_chosenResponse.tip);
        m_previous = _chosenResponse.rating;
        m_interviewer.Expression(_chosenResponse.rating);
        TurnOffOptions();
        FadeOutQuestionText();
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
        m_timerSlider.value = currentTime;

        while (currentTime > 0.0f)
        {
            if (!PauseMenu.IsPaused())
            {
                currentTime -= Time.deltaTime;
                m_timerSlider.value = currentTime;
            }
            yield return null;
        }

        TurnOffOptions();
        FillerText.Silent();
        m_previous = e_rating.AWFUL;
        PlayerWasSilent(m_questionBox.text);
        m_processNextStep.Invoke();
    }

    /// <summary>
    /// Fade Out the buttons, turn off the timer and set the text for the
    /// question box to blank
    /// </summary>
    void TurnOffOptions()
    {
        m_optionPool.TurnOffOptions();
        m_timerSlider.gameObject.SetActive(false);
    }

    /// <summary>
    /// Start question for user
    /// </summary>
    void AskAboutJob()
    {
        m_questionBox.SetText("So do you have any questions about the job" +
            "itself?");
        m_fadeText = StartCoroutine(FadeAsset(m_questionBox, 0.75f, true));

        m_optionPool.Set(m_questionForJob);

        m_endLevel = true;

        StartCoroutine(WaitForAnswer());
    }

    /// <summary>
    /// Process whatever we do next
    /// </summary>
    void ProcessNextStep()
    {
        FadeOutQuestionText();
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
        FadeOutQuestionText();
        yield return new WaitForSecondsRealtime(2);
        m_questionBox.SetText(_fillInText);
        m_fadeText = StartCoroutine(FadeAsset(m_questionBox, m_fadeInSpeed,
            true));
        yield return new WaitForSecondsRealtime(2);
        FadeOutQuestionText();
        yield return new WaitForSecondsRealtime(2);
        m_randomQuestion.Invoke();
    }

    /// <summary>
    /// The couroutine to end the level
    /// </summary>
    /// <returns>yield for 2 seconds</returns>
    IEnumerator EndLevel()
    {
        FadeOutQuestionText();

        yield return new WaitForSecondsRealtime(2);

        string finalChoice = ReturnFinalChosenResults()
            [ReturnFinalChosenResults().Count - 1]
            .playerResponse.response;

        Debug.Log(finalChoice);

        string response = "Nothing? Ok then...";

        foreach (PlayerQuestion question in m_questionForJob)
        {
            Debug.Log(question.response);
            if (finalChoice.Equals(question.question))
            {
                response = question.response;
                break;
            }
        }

        m_questionForJob.Clear();

        //TODO push that player was silent on asking there own question

        m_questionBox.SetText(response);
        m_fadeText = StartCoroutine(FadeAsset(m_questionBox, m_fadeInSpeed,
            true));
        yield return new WaitForSecondsRealtime(3);
        GameObject card = Instantiate(Resources.Load<GameObject>
            ("Prefabs/ScoreCard"),
            transform.position, transform.rotation);
        StartCoroutine(EndInterview(card));
        Destroy(m_timerSlider.gameObject);
    }

    /// <summary>
    /// coroutine to run through the introduction to the questions
    /// </summary>
    /// <returns>1.5 second wait</returns>
    IEnumerator StartInterview()
    {
        StartCoroutine(FadeAsset(transform.parent.GetComponent<Renderer>(),
            m_fadeInSpeed, true));
        string[] introText = LoadIntroText();
        WaitForSeconds waitFor = new WaitForSeconds(1.5f);

        foreach (string line in introText)
        {
            m_questionBox.SetText(line);
            m_fadeText = StartCoroutine(FadeAsset(m_questionBox,
                m_fadeInSpeed, true));
            yield return waitFor;
            FadeOutQuestionText();
            yield return waitFor;
        }
        m_randomQuestion.Invoke();
    }

    /// <summary>
    /// function to end the interview
    /// </summary>
    /// <param name="_card">the card to fade in</param>
    /// <returns>1.5 second wait</returns>
    IEnumerator EndInterview(GameObject _card)
    {
        WaitForSeconds waitFor = new WaitForSeconds(1.5f);

        StartCoroutine(m_optionPool.Clear());

        string[] outroText = LoadOutroText();
        foreach (string line in outroText)
        {
            FadeOutQuestionText();
            yield return waitFor;
            m_questionBox.SetText(line);
            m_fadeText = StartCoroutine(FadeAsset(m_questionBox, m_fadeInSpeed,
                true));
            yield return waitFor;
        }
        _card.GetComponentInChildren<FinalResult>().Display();

        FadeOutQuestionText();
        yield return waitFor;

        Destroy(transform.parent.gameObject);
    }

    /// <summary>
    /// Funtion to call coroutine to fade out question text
    /// </summary>
    void FadeOutQuestionText()
    {
        if (m_fadeText != null)
        {
            StopCoroutine(m_fadeText);
        }
        m_fadeText = StartCoroutine(FadeAsset(m_questionBox,
            m_fadeInSpeed, false));
    }
}