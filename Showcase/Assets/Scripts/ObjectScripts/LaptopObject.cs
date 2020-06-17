using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class LaptopObject : InteractableObjectBase
{
    [SerializeField]
    Button ig_unlock1Button, ig_unlock2Button, ig_unlock3Button, ig_unlock4Button, ig_returnButton;
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
        m_gmscript.SetCurrentHUD(m_gmscript.ig_LaptopPanel);
        m_playerscript.SetCanInteract(false);
        m_playerscript.SetCanCameraMove(false);
        m_playerscript.SetCanPlayerMove(false);
        m_gmscript.m_cmScript.EnableCursor();
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_open", GetComponent<Transform>().position);
    }

    public void ReturnToPlayer()
    {
        m_gmscript.SetCurrentHUD(m_gmscript.ig_PlayerPanel);
        m_playerscript.SetCanInteract(true);
        m_playerscript.SetCanCameraMove(true);
        m_playerscript.SetCanPlayerMove(true);
        m_gmscript.m_cmScript.DisableCursor();
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/tasklist_close", GetComponent<Transform>().position);
    }

    public void Unlock1()
    {
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.FIRST);
        ig_unlock1Button.enabled = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }

    public void Unlock2()
    {
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.SECOND);
        ig_unlock2Button.enabled = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }

    public void Unlock3()
    {
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.THIRD);
        ig_unlock3Button.enabled = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }

    public void Unlock4()
    {
        ConversationStore.RegisterUnlockFlag(e_unlockFlag.FOURTH);
        ig_unlock4Button.enabled = false;
        FMODUnity.RuntimeManager.PlayOneShot("event:/UI/clue_found", GetComponent<Transform>().position);
    }
}
