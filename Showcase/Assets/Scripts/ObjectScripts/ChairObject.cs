using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairObject : InteractableObjectBase
{
    [SerializeField] QuestionManager m_questionManager;
    [SerializeField] GameObject ig_playerobject, ig_sitpoisitonobject;

    BoxCollider m_boxcollider;
    [SerializeField] bool m_isinterviewchair;
    int m_chairchoice = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_boxcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override void Interact()
    {
        Debug.Log("chair");
        if (m_isinterviewchair.Equals(true))
        {
            ig_playerobject.transform.position = ig_sitpoisitonobject.transform.position;
            m_playerscript.SetCanPlayerMove(false);
            WaitingRoomManager.IsInInterview();
            m_boxcollider.enabled = false;


            m_gmscript.SetTaskTrue(7);
            // Start Interview
            Debug.Assert(m_questionManager != null, "There is no " +
                "reference to a Question Manager Object");

            m_questionManager.BeginInterview();
        }
        else
        {
            ig_playerobject.transform.position = ig_sitpoisitonobject.transform.position;
        }
    }


   
}
