using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaitingRoomManager : MonoBehaviour
{
    [Header("Conversations")]
    [SerializeField]
    private bool m_chatAvailable;
    [SerializeField]
    private List<DialogSO> m_availableDialogs;

    private float m_goToInterviewTimer = 10f;


    [SerializeField] private GameObject ig_doorToCoffee;
    [SerializeField] private GameObject ig_interviewDoor;

    [Header("Clock")]
    [SerializeField]
    private float m_waitTimer;
    [SerializeField]
    private float m_hourStart;
    private Vector3 m_rotHour;
    [SerializeField]
    private GameObject ig_Clock;
    [SerializeField]
    private GameObject ig_ExtraClock;
    [SerializeField]
    private GameObject ig_tinyClockHandle;
    [SerializeField]
    private GameObject ig_normalClockHandle;


    [Header("UI")]
    [SerializeField]
    private GameObject ig_textRepresentation;
    [SerializeField]
    private GameObject ig_textBox;
    [SerializeField]
    private GameObject ig_nextText;


    [Header("DEBUG ONLY")]
    [SerializeField]
    private float m_minutes;
    [SerializeField]
    private float m_seconds;
    [SerializeField]
    private DialogSO m_currentDialog;
    [SerializeField]
    private GameObject ig_chair;


    List<DialogSO> m_usedDialogs;
    [SerializeField]
    int m_currentSentence;
    float m_dialogTimer;
    bool m_nextTextShown;
    bool m_activeChat = false;
    bool m_setMoveAgain = false;

    protected PlayerController m_playerscript;

    private GameManagerScript m_gmScript;
    private static bool m_IsSited;
    private static bool m_IsInInterview;
    private bool m_startTimerToGoToInterview =false;

    private WorkerAudio m_audio;

    // Start is called before the first frame updateS
    void Start()
    {
        m_playerscript = FindObjectOfType<PlayerController>();
        PickNextDialog();
        m_usedDialogs = new List<DialogSO>();
        m_dialogTimer = Random.Range(1,3);//random wait timer between 2 dialogs
        m_audio = new WorkerAudio(GetComponent<FMODUnity.StudioEventEmitter>());

        m_gmScript = FindObjectOfType<GameManagerScript>();

        m_rotHour = new Vector3(0, 0, (m_hourStart * 360) / 60);

    }

    // Update is called once per frame
    void Update()
    {
        if (m_activeChat)
        {
            DialogTick();
        }

        TimerTick();
        TimerInClock();
        StopDialog();
    }


    public void DoorClose()
    {
        
        if (ig_doorToCoffee.GetComponent<DoorObject>().Getm_dooropen())
        {
            ig_doorToCoffee.GetComponent<DoorObject>().Setm_dooropen(false);
        }
        StartCoroutine(LockDoor());
    }

    IEnumerator LockDoor()
    {
        yield return new WaitForSeconds(2);
        ig_doorToCoffee.GetComponent<DoorObject>().SetM_lockDoor(true);
        m_activeChat = true;
    }

    public void UnlockDoor()
    {
        ig_doorToCoffee.GetComponent<DoorObject>().SetM_lockDoor(false);
        
    }


    //----------------------------------WAIT TIMER----------------------------------
    /// <summary>
    /// Simple tick for the wait timer
    /// </summary>
    void TimerTick()
    {
        if(m_waitTimer > 0)
        {
            m_waitTimer -= Time.deltaTime;
        }
        else
        {
            if(!m_nextTextShown)
            {
                StartCoroutine(NextText());
                m_nextTextShown = true;
            }
            
            if(!m_setMoveAgain)
            {
                m_playerscript.SetCanPlayerMove(true);
                ig_chair.GetComponent<WaitingChair>().enabled = false;
                m_gmScript.SetTaskTrue(6);
                m_startTimerToGoToInterview = true;
                ig_Clock.GetComponent<ClockManager>().SetTimers(m_hourStart, m_waitTimer);
                //ig_ExtraClock.GetComponent<ClockManager>().SetTimers(m_hourStart, m_waitTimer);
                ig_interviewDoor.GetComponent<InteractableObjectBase>().enabled = true;
               
                if (!m_IsSited)
                {
                    ConversationStore.DidntArrivedInWaitingAreaOnTime();//wasnt sitted on chair when timer ran off
                }
                m_setMoveAgain = true;
            }
            //
            //end scene
        }
    }

    /// <summary>
    /// Uses the timer value to calculate mins and secs
    /// </summary>
    void TimerInClock()
    {
        m_minutes = Mathf.Floor(m_waitTimer / 60);
        m_seconds = Mathf.RoundToInt(m_waitTimer % 60);

        Vector3 _newRot = new Vector3(0, 90, (m_seconds * 360) / 60);
        ig_tinyClockHandle.transform.rotation = Quaternion.Euler(_newRot);

        Vector3 _newRotH = new Vector3(0, 90, (m_minutes * 360) / 60);
        ig_normalClockHandle.transform.rotation = Quaternion.Euler(m_rotHour+_newRotH);
    }

    IEnumerator NextText()
    {
        ig_nextText.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/Dialogue/Interviewer/Extras/next_please", GetComponent<Transform>().position);
        yield return new WaitForSeconds(2);
        ig_nextText.SetActive(false);
    }


    //----------------------------------GO TO INTERVIEW TIMER----------------------------------

    void GoInterviewTimer()
    {
        if(m_goToInterviewTimer>0 && m_startTimerToGoToInterview)
        {
            m_goToInterviewTimer -= Time.deltaTime;
        }
        else
        {
            if(!m_IsInInterview)
            {
                ConversationStore.DidntReachedInterviewerOnTime();//wasnt inside the room after a while
                m_startTimerToGoToInterview = false;
            }
        }
    }



    //----------------------------------DIALOG CONTROLLER----------------------------------

    /// <summary>
    /// Picks a random dialog to run from the available dialog list
    /// </summary>
    void PickNextDialog()
    {
        if(m_currentDialog == null)
        {
            int r = Random.Range(0, m_availableDialogs.Count);
            m_currentDialog = m_availableDialogs[r];
        }
    }

    /// <summary>
    /// Moves the last used dialog into the used list to prevent repeats
    /// </summary>
    /// <param name="_dialog"></param>
    void RemoveDialog(DialogSO _dialog)
    {
        m_availableDialogs.Remove(_dialog);
        m_usedDialogs.Add(_dialog);
        m_currentDialog = null;

    }

    /// <summary>
    /// Updates the UI representation of the dialog text
    /// </summary>
    /// <param name="_newText"></param>
    void UpdateTextDisplay(string _newText)
    {

        ig_textRepresentation.GetComponent<TextMeshProUGUI>().text = _newText;
        
    }

    /// <summary>
    /// reads the next sentence in the dialog scriptable obj
    /// if the current sentence is above the available sentences in the dialog it removes and resets the dialog
    /// </summary>
    void ReadNextSentence()
    {

        if (m_currentSentence >= m_currentDialog.sentences.Count)
        {
            RemoveDialog(m_currentDialog);
            m_dialogTimer = Random.Range(3, 7);//random wait timer between 2 dialogs
            ig_textBox.SetActive(false);
            m_currentSentence = 0;
            PickNextDialog();
        }
        else
        {
           // ig_textBox.SetActive(true);
            m_audio.PlayEvent(m_currentDialog.m_conversationID, m_currentDialog.sentences[m_currentSentence].m_sentenceID);
            UpdateTextDisplay(m_currentDialog.sentences[m_currentSentence].text);
            m_dialogTimer = 1;
            m_currentSentence += 1;
        }
    }


    /// <summary>
    /// Simple tick for the dialog timer
    /// </summary>
    void DialogTick()
    {
        if(m_dialogTimer>0)
        {
            m_dialogTimer -= Time.deltaTime;
        }
        else
        {
            if(!m_audio.IsPlaying())
            {
                ReadNextSentence();
            }
           
        }

    }


    /// <summary>
    /// Stops the dialog when the timer runs out
    /// </summary>
    void StopDialog()
    {
        if(m_waitTimer <= 0 && m_activeChat)
        {
            ig_textBox.SetActive(false);
            m_activeChat = false;
        }
    }



    //----Gets and Sets
    public float Getm_WaitTimer()
    {
        return m_waitTimer;
    }

    public static void IsSitedInWaitingRoom()
    {
        m_IsSited = true;
    }

    public static void IsInInterview()
    {
        m_IsInInterview = true;
        Debug.Log("IS IN INTERVIEW CHECKED");
    }

}
