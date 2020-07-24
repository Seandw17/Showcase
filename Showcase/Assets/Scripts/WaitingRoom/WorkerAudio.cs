using UnityEngine;
using FMODUnity;
using UnityEngine.Assertions;

public class WorkerAudio
{
    // Fmod Instance for this GameObject
    FMODUnity.StudioEventEmitter m_FMODInstance;

    static char[] partChar = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I',
        'J', 'K', 'L', 'M', 'N' };

    public WorkerAudio(FMODUnity.StudioEventEmitter _emitter)
    {
        Assert.IsNotNull(_emitter);
        m_FMODInstance = _emitter;
    }
    
    public void PlayEvent(int _conversationID, int _sentence)
    { 
        char partOfConversation = partChar[_sentence];

        try
        {
            m_FMODInstance.Event = "event:/Dialogue/Worker Conversations/Convo" +
                (_conversationID + 1) + partOfConversation;

            Debug.Log("Playing conversation event " + (_conversationID + 1) +
                partOfConversation);

            PlayAudio(true);
        }
        catch (EventNotFoundException)
        {
            Debug.LogWarning("Couldn't find line that was requested");
        }
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
