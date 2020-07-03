using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LaptopObject : InteractableObjectBase
{
    [SerializeField]
    Button ig_unlock1Button, ig_unlock2Button, ig_unlock3Button, ig_unlock4Button, ig_returnButton, ig_returnButton1, ig_internetButton, ig_webpagereturnButton, ig_webpagereturnButton1, ig_webpagereturnButton2, ig_webpagereturnButton3;

    bool m_openfirst, m_opensecond, m_openthird, _m_openfourth;

    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();   

    }

    public void SetUpButtons(Button _return, Button _unlock1,
        Button _unlock2, Button _unlock3, Button _unlock4, Button _internet, Button _webpagereturn, Button _webpagereturn1, Button _webpagereturn2, Button _webpagereturn3, Button _returnbutton1)
    {
        _return.onClick.AddListener(ReturnToPlayer);
        ig_returnButton = _return;
        _returnbutton1.onClick.AddListener(ReturnToPlayer);
        ig_returnButton1 = _returnbutton1;
        _unlock1.onClick.AddListener(Unlock1);
        ig_unlock1Button = _unlock1;
        _unlock2.onClick.AddListener(Unlock2);
        ig_unlock2Button = _unlock2;
        _unlock3.onClick.AddListener(Unlock3);
        ig_unlock3Button = _unlock3;
        _unlock4.onClick.AddListener(Unlock4);
        ig_unlock4Button = _unlock4;
        _internet.onClick.AddListener(OpenWebPage);
        ig_internetButton = _internet;
        _webpagereturn.onClick.AddListener(ReturnToWebPage);
        ig_webpagereturnButton = _webpagereturn;
        _webpagereturn1.onClick.AddListener(ReturnToWebPage);
        ig_webpagereturnButton1 = _webpagereturn1;
        _webpagereturn2.onClick.AddListener(ReturnToWebPage);
        ig_webpagereturnButton2 = _webpagereturn2;
        _webpagereturn3.onClick.AddListener(ReturnToWebPage);
        ig_webpagereturnButton3 = _webpagereturn3;
    }

    public override void Interact()
    {
        //Gamemanager Script used to set the current hud
        GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.LAPTOP));
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
        GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.WEBSITEMAIN));
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_open", GetComponent<Transform>().position);
    }

    public void ReturnToWebPage()
    {
       GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.WEBSITEMAIN));
       FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_open", GetComponent<Transform>().position);
    }
    public void ReturnToPlayer()
    {
        Debug.Log("Clicked");
        GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.PLAYER));
        m_playerscript.SetCanInteract(true);
        m_playerscript.SetCanCameraMove(true);
        m_playerscript.SetCanPlayerMove(true);
        GameManagerScript.GetCursor().DisableCursor();
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_close", GetComponent<Transform>().position);
    }

    public void Unlock1()
    {
        GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.WEBSITEPRODUCT));
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
        GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.WEBSITEABOUT));
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.SECOND);
        
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }

    public void Unlock3()
    {
        GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.WEBSITECOMMUNITY));
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.THIRD);
       
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }

    public void Unlock4()
    {
        GameManagerScript.SetCurrentHUD(GameManagerScript.ReturnPanel(e_PanelTypes.WEBSITELOOK));
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.FOURTH);
      
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }
}
