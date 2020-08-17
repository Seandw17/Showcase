using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class StaffMemberObject : InteractableObjectBase
{
    public UnityEngine.AI.NavMeshAgent ig_staffAgent { get; private set; }

    bool m_isInteractable = true;
    bool m_staffMemberTriggered = false;
    bool m_staffMemberMoving = false;
    bool m_reachedDoor = false;

    public Transform[] ig_targetPoints; // the patrol points for the AI
    public GameObject ig_staffMemberText;
   
    private int m_destPoint = 0;
    private float m_currentWaitTime = 0.0f;
    public float m_maxWaitTimer = 0.0f;
    private int m_rotationSpeed = 5;
    private bool talked = false;
    private bool arrivedMessage = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        FMODUnity.RuntimeManager.PlayOneShot("event:/Dialogue/barista/how_may_i_help", GetComponent<Transform>().position);
        ig_staffAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        ig_staffAgent.updateRotation = false;
        ig_staffAgent.updatePosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_staffMemberTriggered && !m_staffMemberMoving)
        {
            if(!talked)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Dialogue/barista/youre_here_for_the_interview", GetComponent<Transform>().position);
                talked = true;
            }
            // show the text above the staff member then procede to wait a few seconds before starting the pathfinding.
            m_currentWaitTime += Time.deltaTime;
            int seconds = Mathf.RoundToInt(m_currentWaitTime % 60.0f);
            if (seconds == m_maxWaitTimer)
            {
                m_staffMemberMoving = true;
                ig_staffMemberText.SetActive(false);
            }
        }
        else if (m_staffMemberMoving)
        {
            AIPathfinding();
        }      
    }


    public override void Interact()
    {
        if (m_isInteractable)
        {
            Debug.Log("Staff member triggered");
            m_isInteractable = false;
            m_staffMemberTriggered = true;
            ig_staffMemberText.GetComponentInChildren<TextMeshPro>().text = "Ah you're here for the interview. This way please.";

            m_gmscript.SetTaskTrue(3);
            SetShouldGlow(false);
        }
    }

    // starts the pathfinding towards the door
    void AIPathfinding()
    {
        if (!m_reachedDoor)
        {
            if (ig_targetPoints.Length == 0)
                return;
            else
            {
                if (m_destPoint != ig_targetPoints.Length)
                {
                    ig_staffAgent.SetDestination(ig_targetPoints[m_destPoint].position);

                    Vector3 m_lookPos = ig_targetPoints[m_destPoint].transform.position - ig_staffAgent.transform.position;
                    m_lookPos.y = 0;
                    Quaternion m_targetRotation = Quaternion.LookRotation(m_lookPos);
                    ig_staffAgent.transform.rotation = Quaternion.Slerp(ig_staffAgent.transform.rotation, m_targetRotation, m_rotationSpeed * Time.deltaTime);
                }
            }
        }

        if (!ig_staffAgent.pathPending && ig_staffAgent.remainingDistance < 0.5f)
        {
            if (m_destPoint != ig_targetPoints.Length)
            {
                m_destPoint += 1;
            }
            else if (m_destPoint == ig_targetPoints.Length)
            {
                m_reachedDoor = true;
                m_staffMemberMoving = false;
                m_staffMemberTriggered = false;
                ig_staffMemberText.SetActive(true);
                ig_staffMemberText.GetComponentInChildren<TextMeshPro>().text = "Please enter through here.";

                Vector3 m_rotationVector = ig_staffAgent.transform.rotation.eulerAngles;
                m_rotationVector.y = m_rotationVector.y + 180;
                ig_staffAgent.transform.rotation = Quaternion.Euler(m_rotationVector);

                if(!arrivedMessage)
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Dialogue/barista/please_enter", GetComponent<Transform>().position);
                    arrivedMessage = true;
                }
            }
        }        
    }
}
