using UnityEngine;
using System;

public class QuestionAudio : MonoBehaviour
{
    // Fmod Instance for this GameObject
    FMODUnity.StudioEventEmitter m_FMODInstance;

    private void Awake()
    {
        m_FMODInstance = GetComponent<FMODUnity.StudioEventEmitter>();

    }

    public void PlayNewQuestion(int _questionID, e_rating _rating)
    {
        if (!IsDonePlaying())
        {
            Debug.LogWarning("Audio was already playing when this was called");
            StopAudio();
        }

        string newEvent = "Q" + _questionID + ParseContext(_rating);

        m_FMODInstance.Event = "event:/Dialogue/Interviewer/Questions/" + newEvent;
        m_FMODInstance.Lookup();
        Debug.Log("Playing event: " + newEvent);
        Debug.Assert(m_FMODInstance.Event != "", "An error has occured in " +
            "the setting of the event");
        PlayAudio();
    }

    /// <summary>
    /// Is audio event done playing?
    /// </summary>
    /// <returns>if done</returns>
    public bool IsDonePlaying() => !m_FMODInstance.IsPlaying();

    /// <summary>
    /// Play the event
    /// </summary>
    public void PlayAudio()
    {
        if (!IsDonePlaying())
        {
            Debug.LogWarning("This was called when audio was already running");
        }
        m_FMODInstance.Play();
    }

    /// <summary>
    /// Stop the event
    /// </summary>
    public void StopAudio() => m_FMODInstance.Stop();

    /// <summary>
    /// Play the intro text
    /// </summary>
    public void PlayIntroText()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Play the outro text
    /// </summary>
    public void PlayOutroText()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Play a specific event from the questions directory
    /// </summary>
    /// <param name="_event">the events name</param>
    public void PlayEventFromQuestions(string _event)
    {
        if (!IsDonePlaying())
        {
            Debug.LogWarning("Audio was playing when this was called");
        }
        m_FMODInstance.Event = "event:/Dialogue/Interviewer/Questions/"
            + _event;
        m_FMODInstance.Lookup();
        PlayAudio();
    }

    /// <summary>
    /// Play the response to a player question
    /// </summary>
    /// <param name="ID"></param>
    public void PlayResponseToPlayerQuestion(int _ID)
    {
        if (!IsDonePlaying())
        {
            Debug.LogWarning("Audio was playing when this was called");
        }

        if (_ID != 0)
        {
            m_FMODInstance.Event = "event:/Dialogue/Interviewer/Answers/answer_"
                + _ID;
        }
        else
        {
            m_FMODInstance.Event = "event:/Dialogue/Interviewer/Questions" +
                "/nothing_ok_then";
        }
        m_FMODInstance.Lookup();
        PlayAudio();
    }

    /// <summary>
    /// Function to parse the audio context
    /// </summary>
    /// <param name="_context">the past rating</param>
    /// <returns>a char that corrosponds</returns>
    char ParseContext(e_rating _context)
    {
        switch (_context)
        {
            case e_rating.NONE:
                return 'A';
            case e_rating.GREAT:
                return 'B';
            case e_rating.GOOD:
                return 'C';
            case e_rating.OK:
                return 'D';
            case e_rating.BAD:
                return 'E';
            case e_rating.AWFUL:
                return 'F';
        }

        throw new Exception("An illegal value has been passed to the " +
            "context parser");
    }
}
