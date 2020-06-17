using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairObject : InteractableObjectBase
{
    [SerializeField]
    GameObject ig_playerObject, ig_sitpositionObject;

   

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
                    transform.position = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
                    break;
                }
            case 2:
                {
                    ig_playerObject.transform.position = ig_sitpositionObject.transform.position;
                    m_playerscript.SetCanPlayerMove(false);
                    m_playerscript.SetIsInInterview(true);

                   
                    //TODO - Set player Position to be facing the interviewer
                    break;
                }
            case 3:
                {
                    transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z); 
                    break;
                }
            
        }
        if (m_chairchoice >= 3)
        {
            m_chairchoice = 0;
        }
        
    }


   
}
