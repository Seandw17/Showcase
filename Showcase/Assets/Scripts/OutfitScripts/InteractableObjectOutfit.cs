using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableObjectOutfit : InteractableObjectBase
{
    GameObject ig_OutfitManager;
    CursorController m_cmScript;
    OutfitManager m_omScript;

    [SerializeField] GameObject m_outfitUIObject, m_noResearchDoneWarningUIObject;

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

    IEnumerator DisplayWarning()
    {
        yield return new WaitForSeconds(2.0f);
        GameManagerScript.SetHUDBack();
    }

    // when the player interacts with the wardrobe
    public override void Interact()
    {
        if (m_gmscript.m_objectiveboolarray[0].Equals(true))
        {
            GameManagerScript.SetNewHUD(m_outfitUIObject);
            m_cmScript.EnableCursor();

            // for now it just disables the playerscript. There might be a better way of doing this
            m_playerscript.enabled = false;
        }
        else
        {
            Debug.Log("Do Some Research");
            GameManagerScript.SetNewHUD(m_noResearchDoneWarningUIObject);
            // fades in text over the "5" seconds
            StartCoroutine(FadeIn.AssetInOut(GameManagerScript
                .ReturnCurrentHUD().GetComponentInChildren
                <TextMeshProUGUI>(), 5, 2));
            // also waits the above seconds +1 before setting the hud back
            StartCoroutine(DisplayWarning());
        }
    }
}
