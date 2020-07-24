using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicantInInterview : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent ig_navAgent { get; private set; }
    public GameObject ig_standingUp;
    public GameObject ig_siting;
    private WaitingRoomManager roomManager;
    [SerializeField] private GameObject ig_doorToInterview;


    bool m_reachedLocation = false;
    public Transform[] ig_targetPoints; // the patrol points for the AI

    private bool startMove = false;
    private bool triggerMove = false;
    private int m_destPoint = 0;
    // Start is called before the first frame update
    void Start()
    {
        ig_navAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        roomManager = GameObject.FindGameObjectWithTag("WaitingRoomManager").GetComponent<WaitingRoomManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckTimer();
        AIPathfinding();
    }

    void CheckTimer()
    {
        if(roomManager.Getm_WaitTimer() <= 0 && !triggerMove)
        {
            //time to move out
            triggerMove = true;
            StartCoroutine(MoveAI());
           
        }
    }

    IEnumerator MoveAI()
    {
        //open the door
        ig_doorToInterview.GetComponent<DoorObject>().Setm_dooropenApplicant(true);
        ig_siting.SetActive(false);
        ig_standingUp.SetActive(true);
         //Wait a few seconds
         yield return new WaitForSeconds(2);
        //Move Ai
        startMove = true;


    }


    void AIPathfinding()
    {
        if(startMove)
        {
            if (!m_reachedLocation)
            {
                if (ig_targetPoints.Length == 0)
                    return;
                else
                {
                    if (m_destPoint != ig_targetPoints.Length)
                        ig_navAgent.SetDestination(ig_targetPoints[m_destPoint].position);
                }
            }

            if (!ig_navAgent.pathPending && ig_navAgent.remainingDistance < 0.5f)
            {
                if (m_destPoint != ig_targetPoints.Length)
                {
                    m_destPoint += 1;
                }
                else if (m_destPoint == ig_targetPoints.Length)
                {
                    m_reachedLocation = true;
                    this.gameObject.SetActive(false);
                }
            }
        }
        
    }
}
