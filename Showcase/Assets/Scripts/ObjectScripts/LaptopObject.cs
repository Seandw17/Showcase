using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LaptopObject : InteractableObjectBase
{
    [SerializeField] GameObject m_laptopPanelHome, m_websiteMain, m_websiteCommunity, m_websiteLooking, m_websiteProduct, m_websiteAbout;

    bool m_openfirst, m_opensecond, m_openthird, _m_openfourth;

    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();   

    }

    public override void Interact()
    {
        //Gamemanager Script used to set the current hud
        GameManagerScript.SetNewHUD(m_laptopPanelHome);
        //a bool to stop the player from interacting
        m_playerscript.SetCanInteract(false);
        //a bool to stop the player camera from moving
        m_playerscript.SetCanCameraMove(false);
        //a bool to stop the player from moving
        m_playerscript.SetCanPlayerMove(false);
        //Set the cursor active
        GameManagerScript.GetCursor().EnableCursor();
        //Play Sound
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_open", GetComponent<Transform>().position);
    }

    public void OpenWebPage()
    {
        GameManagerScript.SetNewHUD(m_websiteMain);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_open", GetComponent<Transform>().position);
    }

    /*
    public void ReturnToWebPage()
    {
       GameManagerScript.SetNewHUD(m_websiteMain);
       FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_open", GetComponent<Transform>().position);
    }*/

    public void ReturnToPlayer()
    {
        Debug.Log("Clicked");
        GameManagerScript.SetHUDBack();
        m_playerscript.SetCanInteract(true);
        m_playerscript.SetCanCameraMove(true);
        m_playerscript.SetCanPlayerMove(true);
        GameManagerScript.GetCursor().DisableCursor();
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_close", GetComponent<Transform>().position);
    }

    public void Unlock1()
    {
        GameManagerScript.SetNewHUD(m_websiteProduct);
        if (m_openfirst.Equals(false))
        {
            ConversationStore.RegisterUnlockFlag(e_unlockFlag.FIRST);
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
            m_openfirst = true;
            m_gmscript.SetTaskTrue(0);
        }
    }

    public void Unlock2()
    {
        GameManagerScript.SetNewHUD(m_websiteAbout);
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.SECOND);
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }

    public void Unlock3()
    {
        GameManagerScript.SetNewHUD(m_websiteCommunity);
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.THIRD);
       
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }

    public void Unlock4()
    {
        GameManagerScript.SetNewHUD(m_websiteLooking);
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.FOURTH);
      
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }
}
