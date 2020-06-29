using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]

public class StaffMemberObject : InteractableObjectBase
{
    public UnityEngine.AI.NavMeshAgent ig_staffAgent { get; private set; }

    bool m_staffMemberTriggered = false;
    bool m_staffMemberMoving = false;

    public Transform[] ig_targetPoints; // the patrol points for the AI
    private int m_destPoint = 0;

    private float m_currentWaitTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        ig_staffAgent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        ig_staffAgent.updateRotation = false;
        ig_staffAgent.updatePosition = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_staffMemberTriggered && !m_staffMemberMoving)
        {
            // show the text above the staff member then procede to wait a few seconds before starting the pathfinding.
            m_currentWaitTime += Time.deltaTime;
            int seconds = Mathf.RoundToInt(m_currentWaitTime % 60.0f);
            if (seconds == 5)
            {
                m_staffMemberMoving = true;
                Debug.Log("Staff member begun pathfinding");
            }
        }
        else if (m_staffMemberMoving)
        {
            AIPathfinding();
        }
        
    }

    public override void Interact()
    {
        Debug.Log("Staff member triggered");
        m_staffMemberTriggered = true;
    }

    void AIPathfinding()
    {
        if (ig_targetPoints.Length == 0)
            return;
        else
        {
            ig_staffAgent.SetDestination(ig_targetPoints[m_destPoint].position);
        }


        if (!ig_staffAgent.pathPending && ig_staffAgent.remainingDistance < 0.5f)
        {
            m_destPoint += 1;
            if (m_destPoint > ig_targetPoints.Length)
            {
                Debug.Log("Staff member reached door");
                m_staffMemberMoving = false;
                m_staffMemberTriggered = false;
            }
        }


        
    }
}
