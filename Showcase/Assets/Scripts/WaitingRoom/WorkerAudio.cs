using UnityEngine;
using System;
using FMODUnity;
using UnityEngine.Assertions;

public class WorkerAudio
{
    // Fmod Instance for this GameObject
    FMODUnity.StudioEventEmitter m_FMODInstance;

    public WorkerAudio(FMODUnity.StudioEventEmitter _emitter)
    {
        Assert.IsNotNull(_emitter);
        m_FMODInstance = _emitter;
    }
    /// <summary>
    /// Specify the new event, and play
    /// </summary>
    /// <param name="_name">name of the event</param>
    public void PlayEvent(int _conversationID, char _sentence)
    {
        m_FMODInstance.Event = "event:/Dialogue/Workers/Convo" +
            _conversationID + _sentence;

        Debug.Log("Playing conversation event " + _conversationID + _sentence);

        PlayAudio(true);
    }

    /// <summary>
    /// Play audio
    /// </summary>
    /// <param name="_lookUp">do we want to lookup audio?</param>
    public void PlayAudio(bool _lookUp)
    {
        if (IsPlaying())
        {
            Debug.LogWarning("Audio was playing when this was called");
            StopAudio();
        }

        try
        {
            if (_lookUp)
            {
                m_FMODInstance.Lookup();
            }
            m_FMODInstance.Play();
        }
        catch (EventNotFoundException)
        {
            Debug.LogWarning("Requested audio could not be found");
        }
        
    }

    /// <summary>
    /// Is the audio playing?
    /// </summary>
    /// <returns>true if the audio is playing</returns>
    public bool IsPlaying() => m_FMODInstance.IsPlaying();

    /// <summary>
    /// Stop the audio
    /// </summary>
    public void StopAudio() => m_FMODInstance.Stop();
}
