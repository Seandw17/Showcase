using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairObject : InteractableObjectBase
{
    [SerializeField] w_QuestionManager m_questionManager;
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
            m_chairchoice++;
            switch (m_chairchoice)
            {
                case 1:
                    {
                        transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
                        break;
                    }
                case 2:
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

                        break;
                    }
                case 3:
                    {
                        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
                        break;
                    }

            }
            if (m_chairchoice >= 3)
            {
                m_chairchoice = 0;
            }
        }
        else
        {
            ig_playerobject.transform.position = ig_sitpoisitonobject.transform.position;
        }
    }


   
}
