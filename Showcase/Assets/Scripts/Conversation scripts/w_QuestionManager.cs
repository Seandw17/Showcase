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
        m_questions = w_CSVLoader.LoadQuestionData("IQuestions");
        Debug.Log(m_questions.Count);
        // questions for job / brand
        w_CSVLoader.LoadInPlayerQuestions("PQuestions",
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

        m_timerSlider.maxValue = m_timeBetweenQuestions;
        m_timerSlider.gameObject.SetActive(false);

        m_processNextStep = new UnityEvent();
        m_processNextStep.AddListener(ProcessNextStep);

        m_randomQuestion = new UnityEvent();
        m_randomQuestion.AddListener(LoadRandomQuestion);

        m_fillerText = new FillerText();

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

        m_previous = _chosenResponse.rating;

        TurnOffButtons();

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

        TurnOffButtons();

        ConversationStore.PlayerWasSilent(m_questionBox.text);
        m_processNextStep.Invoke();
    }

    void TurnOffButtons()
    {
        // Turn of buttons for now
        foreach (OptionData button in m_buttonPool)
        {
            StartCoroutine(button.setInactive());
        }
        m_questionBox.SetText("");
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
            }
        }

        m_questionBox.SetText(response);

        yield return new WaitForSeconds(2);

        Instantiate(Resources.Load<GameObject>("Prefabs/ScoreCard"),
                transform.position, transform.rotation);
        Destroy(m_timerSlider.gameObject, 2.0f);
        Destroy(gameObject, 2.0f);
    }
}