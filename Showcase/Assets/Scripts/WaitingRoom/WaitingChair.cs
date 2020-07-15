﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingChair : InteractableObjectBase
{

    

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
        m_playerscript.SetCanPlayerMove(false);
        Vector3 pos = transform.position;
        pos += new Vector3(0.5f, 1, 0.5f);
        m_playerscript.gameObject.transform.position = pos;
        WaitingRoomManager.IsSitedInWaitingRoom();
        Debug.Log("IS SITTED CHECKED");
        m_gmscript.SetTaskTrue(5);

        
    }

}