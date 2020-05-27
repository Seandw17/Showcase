using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectOutfit : InteractableObjectBase
{
    GameObject ig_OutfitManager;
    CursorController m_cmScript;
    OutfitManager m_omScript;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        ig_OutfitManager = GameObject.Find("OutfitManager");
        m_cmScript = ig_OutfitManager.GetComponent<CursorController>();
        m_omScript = ig_OutfitManager.GetComponent<OutfitManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // when the player interacts with the wardrobe
    public override void Interact()
    {
        m_gmscript.SetCurrentHUD(m_gmscript.ig_WardrobePanel);
        m_cmScript.EnableCursor();

        // for now it just disables the playerscript. There might be a better way of doing this
        m_playerscript.enabled = false;
    }
}
