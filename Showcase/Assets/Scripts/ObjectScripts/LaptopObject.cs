using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LaptopObject : InteractableObjectBase
{
    [SerializeField] GameObject m_laptopPanelHome, m_websiteMain, m_websiteCommunity, m_websiteLooking, m_websiteProduct, m_websiteAbout, m_websiteProductAnswer;
    [SerializeField] List<GameObject> blockouts;
    [SerializeField] Button m_unlockfour, m_unlocktwo, m_unlockthree;
    

    
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
    }

    public void OpenWebPage()
    {
        GameManagerScript.SetNewHUD(m_websiteMain);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
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
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
    }

    public void OpenProductPage()
    {
        GameManagerScript.SetNewHUD(m_websiteProduct);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
    }

    public void OpenAboutPage()
    {
        GameManagerScript.SetNewHUD(m_websiteAbout);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
    }

    public void OpenCommunityPage()
    {
        GameManagerScript.SetNewHUD(m_websiteCommunity);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
    }

    public void OpenLookingPage()
    {
        GameManagerScript.SetNewHUD(m_websiteLooking);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
    }

    public void OpenProductAnswerPage()
    {
        GameManagerScript.SetNewHUD(m_websiteProductAnswer);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
        Unlock1();
    }

    public void Unlock1()
    {
       
         ConversationStore.RegisterUnlockFlag(e_unlockFlag.FIRST);
       
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
         FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
        
         if (m_gmscript.m_objectiveboolarray[0].Equals(false))
         {
              m_gmscript.SetTaskTrue(0);
         }
        
    }

    public void Unlock2()
    {
        
            ConversationStore.RegisterUnlockFlag(e_unlockFlag.SECOND);
        blockouts[1].SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
            FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
           
            if (m_gmscript.m_objectiveboolarray[0].Equals(false))
            {
                m_gmscript.SetTaskTrue(0);
            }
        m_unlocktwo.gameObject.SetActive(false);
        
    }

    public void Unlock3()
    {
        
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.THIRD);
        blockouts[2].SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
            
       if (m_gmscript.m_objectiveboolarray[0].Equals(false))
       {
           m_gmscript.SetTaskTrue(0);
       }
        m_unlockthree.gameObject.SetActive(false);
        
    }

    public void Unlock4()
    {
       ConversationStore.RegisterUnlockFlag(e_unlockFlag.FOURTH);
       blockouts[3].SetActive(false);
       FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
       FMODUnity.RuntimeManager.PlayOneShot("event:/UI/mouse_click", GetComponent<Transform>().position);
            
       if (m_gmscript.m_objectiveboolarray[0].Equals(false))
       {
          m_gmscript.SetTaskTrue(0);
       }     
       m_unlockfour.gameObject.SetActive(false);

    }
}
