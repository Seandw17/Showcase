using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineInteract : InteractableObjectBase
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
        m_gmscript.SetCurrentHUD(m_gmscript.ig_magazineWaitingRoom);
        m_playerscript.SetCanCameraMove(false);
        m_gmscript.m_cmScript.EnableCursor();
        m_playerscript.SetCanInteract(false);
    }

    public void ResetMagazineView()
    {
        m_gmscript.SetCurrentHUD(m_gmscript.ig_PlayerPanel);
        m_playerscript.SetCanCameraMove(true);
        m_gmscript.m_cmScript.DisableCursor();
        m_playerscript.SetCanInteract(true);
    }
}
