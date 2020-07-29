using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioObject : InteractableObjectBase
{

    int m_musicchoice = 0;

    
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
        m_musicchoice++;
        switch (m_musicchoice)
        {
            case 1:
                {
                    FMODUnity.RuntimeManager.PlayOneShot("event:/Music/Chillstep", GetComponent<Transform>().position);
                    //TODO - Add in FMOD code to play music here
                    break;
                }
            case 2:
                {
                   
                    break;
                }
        }
        if (m_musicchoice >= 2)
        {
            m_musicchoice = 1;
        }
        //TODO - Add code to put m_musicchoice back to the beginning
    }
}
