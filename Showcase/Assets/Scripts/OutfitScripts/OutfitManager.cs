using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitManager : MonoBehaviour
{
    // temp names for the outfits as well as a temp amount of outfits
    [SerializeField]
    enum e_Outfits { OUTFIT1, OUTFIT2 , OUTFIT3 };
    e_Outfits m_selectedOutfit;

    GameObject[] ig_Outfit;
    GameObject ig_Player;

    protected PlayerController m_playerscript;
    CursorController m_cmScript;


    // Start is called before the first frame update
    void Start()
    {
        ig_Outfit = GameObject.FindGameObjectsWithTag("Outfit");

        ig_Player = GameObject.Find("Player");
        m_playerscript = ig_Player.GetComponent<PlayerController>();

        m_cmScript = GetComponent<CursorController>();

        for (int i = 0; i < ig_Outfit.Length; i++)
        {
            Button m_tempButton = ig_Outfit[i].GetComponent<Button>();
            int m_tempi = i;
            m_tempButton.onClick.AddListener(() => OutfitClicked(m_tempi));
        }

        ig_Outfit[0].transform.parent.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    // when an outfity is clicked, debug.log what one was clicked and save it as the selected outfit
    void OutfitClicked(int buttonNo)
    {
        m_selectedOutfit = (e_Outfits)buttonNo;
        ig_Outfit[0].transform.parent.transform.parent.gameObject.SetActive(false);
        m_playerscript.enabled = true;
        m_cmScript.DisableCursor();

        Debug.Log("Selected outfit is " + m_selectedOutfit);
    }


    // call the stuff needed to re-enable movement and remove the outfit selectino from the hud
    void RenablePlayerMovement()
    {
        Debug.Log(m_selectedOutfit);
    }
}
