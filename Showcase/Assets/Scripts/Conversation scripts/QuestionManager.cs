using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static FadeIn;
using static CSVLoader;
using static ConversationStore;

// Author: Alec

/// <summary>
/// Class to manage the gameloop of the question stage of the interviewer stage
/// </summary>
public class QuestionManager : MonoBehaviour
{
    TextMeshPro m_questionBox;

    List<QuestionData> m_questions;
    List<PlayerQuestion> m_questionForJob;

    QuestionAudio m_QuestionAudio;

    bool m_endLevel;

    UnityEvent m_processNextStep;

    int m_currentQuestion, m_responseID;

    e_rating m_previous = e_rating.NONE;

    OptionPool m_optionPool;

    Slider m_timerSlider;

    TextMeshProUGUI m_progressText;

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

    /// <summary>
    /// If true, forces interview to start on load, for testing purposes
    /// </summary>
    [SerializeField] bool m_forceInterviewToStart;

    Coroutine m_waitForAnswer, m_fadeText;

    // Start is called before the first frame update
    void Start()
    {
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
        m_timerSlider = Instantiate(Resources.Load<GameObject>
            ("Prefabs/Timer")).GetComponentInChildren<Slider>();
        m_timerSlider.maxValue = m_timeBetweenQuestions;
        m_timerSlider.gameObject.SetActive(false);

        m_QuestionAudio = gameObject.AddComponent<QuestionAudio>();

        // set up events 
        m_processNextStep = new UnityEvent();
        m_processNextStep.AddListener(ProcessNextStep);

        SetAlphaToZero(transform.parent.GetComponent<Renderer>().material);
        SetAlphaToZero(m_questionBox);

        m_progressText = m_timerSlider.transform.root.gameObject
            .GetComponentInChildren<TextMeshProUGUI>();
        m_progressText.SetText("");

        if (m_forceInterviewToStart)
        {
            Init();
            StartCoroutine(StartInterview());
            RegisterUnlockFlag(e_unlockFlag.FIRST);
        }
        else
        {
            enabled = false;
        }
    }

    /// <summary>
    /// Begin the interview
    /// </summary>
    public void BeginInterview()
    {
        Debug.Assert(m_interviewer != null, "No reference to interviewer " +
            "exists");
        StartCoroutine(StartInterview());
    }

    /// <summary>
    /// Force stop fade coroutine
    /// </summary>
    void StopFade()
    {
        if (m_fadeText != null)
        {
            StopCoroutine(m_fadeText);
        }
    }

    /// <summary>
    /// Load a random question
    /// </summary>
    IEnumerator LoadRandomQuestion()
    {
        Debug.Log(m_questions.Count);

        m_currentQuestion++;
        if (m_currentQuestion > m_questionsToAsk)
        {
            m_processNextStep.Invoke();

            Destroy(m_progressText);
        }
        else
        {
            Debug.Log(m_questions.Count);

            // retrieve data
            int nextQuestion = Random.Range(0, m_questions.Count
                - 1);

            List<Questionresponse> playerResponses =
                m_questions[nextQuestion].options;
            string questionToDisplay =
                m_questions[nextQuestion].questions[m_previous];

            // check we have returned a value
            Debug.Assert(!questionToDisplay.Equals(new QuestionData()),
                "An error has occured finding the question");

            // use values to set data
            m_questionBox.SetText(questionToDisplay);
            StopFade();
            m_fadeText = StartCoroutine(FadeAsset(m_questionBox,
                m_fadeInSpeed, true));
            m_QuestionAudio.PlayNewQuestion(m_questions[nextQuestion].ID,
                m_previous);

            // wait for audio
            while (!m_QuestionAudio.IsDonePlaying())
            {
                yield return null;
            }

            m_optionPool.Set(playerResponses, m_questions[nextQuestion]);

            Debug.Log("Chose Question: " + questionToDisplay);

            // remove our question to prevent repeated valeus
            m_questions.RemoveAt(nextQuestion);

            m_waitForAnswer = StartCoroutine(WaitForAnswer());

            m_progressText.SetText("Question " + m_currentQuestion +
                " of " + m_questionsToAsk);
        }
    }

    /// <summary>
    /// Process the result of our question, attach this to a button
    /// </summary>
    public void ProcessQuestionResult(Questionresponse _chosenResponse, int _ID)
    {
        StopCoroutine(m_waitForAnswer);
        m_timerSlider.gameObject.SetActive(false);
        ProcessAnswer(_chosenResponse,
            m_questionBox.text);
        if ((int)_chosenResponse.rating < 4)
        {
            AddTip(_chosenResponse.tip);
        }
        m_previous = _chosenResponse.rating;
        m_interviewer.Expression(_chosenResponse.rating);
        TurnOffOptions();
        FadeOutQuestionText();
        m_processNextStep.Invoke();
        m_responseID = _ID;
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
        m_previous = e_rating.AWFUL;
        PlayerWasSilent(m_questionBox.text);
        m_responseID = m_questionForJob.Count + 1;
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
    IEnumerator AskAboutJob()
    {
        m_questionBox.SetText("So do you have any questions about the job?");
        StopFade();
        m_fadeText = StartCoroutine(FadeAsset(m_questionBox, 0.75f, true));

        Debug.Assert(!IsOnlyNoneFlag(),
            "Somehow the player has reached the end with " +
            "only none as there unlock flag");

        m_QuestionAudio.PlayEventFromQuestions("any_questions");

        while (!m_QuestionAudio.IsDonePlaying())
        {
            yield return null;
        }

        m_optionPool.Set(m_questionForJob);

        m_endLevel = true;

        m_waitForAnswer = StartCoroutine(WaitForAnswer());
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
            StartCoroutine(AskAboutJob());
        }
        else { StartCoroutine(FillInTime()); }
    }

    IEnumerator FillInTime()
    {
        yield return new WaitForSecondsRealtime(2);
        StartCoroutine(LoadRandomQuestion());
    }

    /// <summary>
    /// The couroutine to end the level
    /// </summary>
    /// <returns>yield for 2 seconds</returns>
    IEnumerator EndLevel()
    {
        FadeOutQuestionText();

        yield return new WaitForSecondsRealtime(2);

        string response = "Nothing? Ok then...";
        if (m_responseID != m_questionForJob.Count + 1)
        {
            response = m_questionForJob[m_responseID].response;
            m_QuestionAudio.PlayResponseToPlayerQuestion(m_responseID + 1);
        }
        else
        {
            AddTip(e_tipCategories.NOTASKING);
            m_QuestionAudio.PlayResponseToPlayerQuestion(0);
        }

        while (m_QuestionAudio.IsDonePlaying())
        {
            yield return null;
        }

        m_questionForJob.Clear();

        m_questionBox.SetText(response);
        StopFade();
        m_fadeText = StartCoroutine(FadeAsset(m_questionBox, m_fadeInSpeed,
            true));
        yield return new WaitForSecondsRealtime(3);
        GameObject card = Instantiate(Resources.Load<GameObject>
            ("Prefabs/ScoreCard"));
        Vector3 playerPos = Camera.main.gameObject.transform.root.position;
        card.transform.position = new Vector3(
            playerPos.x - 0.5f,
            playerPos.y + 1f,
            playerPos.z);
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

        for (int index = 0; index < introText.Length; index++)
        {
            m_questionBox.SetText(introText[index]);
            m_QuestionAudio.PlayIntro(index + 1);
            StopFade();
            m_fadeText = StartCoroutine(FadeAsset(m_questionBox,
                m_fadeInSpeed, true));
            while (!m_QuestionAudio.IsDonePlaying())
            {
                yield return null;
            }
            FadeOutQuestionText();
            yield return waitFor;
        }

        StartCoroutine(LoadRandomQuestion());
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

        for (int index = 0; index < outroText.Length; index++)
        {
            FadeOutQuestionText();
            yield return waitFor;
            m_QuestionAudio.PlayOutro(index + 1);
            m_questionBox.SetText(outroText[index]);
            StopFade();
            m_fadeText = StartCoroutine(FadeAsset(m_questionBox, m_fadeInSpeed,
                true));
            while (!m_QuestionAudio.IsDonePlaying())
            {
                yield return null;
            }
        }

        FadeOutQuestionText();
        yield return waitFor;

        _card.GetComponent<ScoreCard>().TurnOn();
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