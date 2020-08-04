using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using static CSVLoader;

// Script to test that the audio matches the text
public class AudioTest : MonoBehaviour
{
    IEnumerator current;

    TextMeshProUGUI m_testText;

    FMODUnity.StudioEventEmitter m_eventEmitter;

    private void Start()
    {
        m_testText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        m_eventEmitter = GetComponent<FMODUnity.StudioEventEmitter>();
    }

    private void Update()
    {
        string input = Input.inputString;

        if (!input.Equals(""))
        {
            if (current != null)
            {
                StopCoroutine(current);
            }

            switch (input)
            {
                case "i":
                    current = TestIntroText();
                    break;
                case "o":
                    current = TestOutroText();
                    break;
                case "p":
                    current = TestPlayerQuestions();
                    break;
                case "q":
                    current = TextInterviewQuestions();
                    break;
                case "e":
                    current = TestEmployeeText();
                    break;
            }

            if (current != null)
            {
                StartCoroutine(current);
            }
        }

    }

    IEnumerator TestIntroText()
    {
        QuestionAudio audio = new QuestionAudio
            (m_eventEmitter) ;
        string[] introText = LoadIntroText();

        for (int index = 0; index < introText.Length; index++)
        {
            m_testText.SetText(introText[index]);

            audio.PlayIntro(index + 1);

            while (!audio.IsDonePlaying())
            {
                yield return null;
            }

            yield return null;
        }
    }

    IEnumerator TestOutroText()
    {
        QuestionAudio audio = new QuestionAudio
            (m_eventEmitter);
        string[] outroText = LoadOutroText();

        for (int index = 0; index < outroText.Length; index++)
        {
            m_testText.SetText(outroText[index]);

            audio.PlayOutro(index + 1);

            while (!audio.IsDonePlaying())
            {
                yield return null;
            }

            yield return null;
        }
    }

    IEnumerator TestPlayerQuestions()
    {
        QuestionAudio audio = new QuestionAudio(m_eventEmitter);
        List<PlayerQuestion> questionForJob;
        LoadInPlayerQuestions("PQuestions", out questionForJob);

        for (int index = 0; index < 5; index++)
        {
            if (index != 0)
            {
                m_testText.SetText(questionForJob[index].response);
                audio.PlayResponseToPlayerQuestion(index + 1);
            }
            else
            {
                m_testText.SetText("Nothing? Ok then...");
                audio.PlayResponseToPlayerQuestion(0);
            }
            
            while (!audio.IsDonePlaying())
            {
                yield return null;
            }
        }

    }

    IEnumerator TextInterviewQuestions()
    {
        QuestionAudio audio = new QuestionAudio(m_eventEmitter);
        List<QuestionData> questions = LoadQuestionData("IQuestions");

        foreach (QuestionData question in questions)
        {
            foreach (e_rating rating in Enum.GetValues(typeof(e_rating)))
            {
                m_testText.SetText(question.questions[rating]);
                audio.PlayNewQuestion(question.ID, rating);

                while (!audio.IsDonePlaying())
                {
                    yield return null;
                }
            }
        }
    }

    IEnumerator TestEmployeeText()
    {
        throw new NotImplementedException();
    }
}
