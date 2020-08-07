using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calander : InteractableObjectBase
{
    //Calander UI
    [SerializeField] GameObject m_calanderPanel;
   
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayCalander()
    {
        GameManagerScript.SetNewHUD(m_calanderPanel);
        m_playerscript.SetCanPlayerMove(false);
        m_playerscript.SetCanInteract(false);
        m_playerscript.SetCanCameraMove(false);
    }

    public void ReturnToPlayer()
    {
        GameManagerScript.SetHUDBack();
        m_playerscript.SetCanPlayerMove(true);
        m_playerscript.SetCanInteract(true);
        m_playerscript.SetCanCameraMove(true);
    }

    public override void Interact()
    {
        DisplayCalander();
    }
}
