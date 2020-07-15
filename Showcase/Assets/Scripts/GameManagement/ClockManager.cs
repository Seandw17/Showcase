using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : MonoBehaviour
{
    [SerializeField]
    private bool m_CheckTimer;

    [SerializeField]
    private float m_hourStart;
    [SerializeField]
    private float m_waitTimer;
    private float m_generalTimer;
    [SerializeField]
    private bool m_tickTock;


    [SerializeField]
    private GameObject ig_tinyClockHandle;
    [SerializeField]
    private GameObject ig_normalClockHandle;


    private Vector3 m_rotHour;
    private float m_minutes;
    private float m_seconds;

    // Start is called before the first frame update
    void Start()
    {
        if(m_tickTock)
        {
            m_rotHour = new Vector3(0, 0, (m_hourStart * 360) / 60);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_tickTock)
        {
            TimerInClock();

            if(m_CheckTimer)
            {
                if(m_generalTimer <= -m_waitTimer)
                {
                    ConversationStore.DidntArrivedToShopOnTime(); //took too long to get out of the room
                    Debug.Log("DELAYED FOR INTERVIEW");
                    m_CheckTimer = false;
                }
            }
        }
    }

    /// <summary>
    /// Uses the timer value to calculate mins and secs
    /// </summary>
    void TimerInClock()
    {
        m_generalTimer -= Time.deltaTime;
        m_minutes = Mathf.Floor(m_generalTimer / 60);
        m_seconds = Mathf.RoundToInt(m_generalTimer % 60);

        Vector3 _newRot = new Vector3(0, 90, (m_seconds * 360) / 60);
        ig_tinyClockHandle.transform.rotation = Quaternion.Euler(_newRot);

        Vector3 _newRotH = new Vector3(0, 90, (m_minutes * 360) / 60);
        ig_normalClockHandle.transform.rotation = Quaternion.Euler(m_rotHour + _newRotH);
    }


    public void SetTimers(float _hour, float _min)
    {
        m_hourStart = _hour;
        m_rotHour = new Vector3(0, 0, (m_hourStart * 360) / 60);

        m_generalTimer = _min;
        m_tickTock = true;
    }
}
