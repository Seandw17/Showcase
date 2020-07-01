using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairObject : InteractableObjectBase
{
    [SerializeField] w_QuestionManager m_questionManager;

    int m_chairchoice = 0;
   
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override void Interact()
    {
        Debug.Log("chair");
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
                    //ig_playerObject.transform.position = ig_sitpositionObject.transform.position;
                    //m_playerscript.m_camera.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);

                    m_playerscript.SetCanPlayerMove(false);
                    //m_playerscript.SetIsInInterview(true);

                    // Start Interview
                    Debug.Assert(m_questionManager != null, "There is no " +
                        "reference to a Question Manager Object");

                    m_questionManager.BeginInterview();

                    break;
                }
            case 3:
                {
                    transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z + 0.5f); 
                    break;
                }
            
        }
        if (m_chairchoice >= 3)
        {
            m_chairchoice = 0;
        }
        
    }


   
}
