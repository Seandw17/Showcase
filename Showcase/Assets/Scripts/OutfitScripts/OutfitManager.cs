using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitManager : MonoBehaviour
{
    // temp names for the outfits as well as a temp amount of outfits
    enum e_Outfits { CASUAL, SMART_CASUAL, SMART };
    e_Outfits m_selectedOutfit;
    static int m_selectedOutfitScore;

    GameObject[] ig_Outfit;
    GameObject ig_Player;

    [SerializeField]
    Material[] m_outfitMats;

    protected PlayerController m_playerscript;
    CursorController m_cmScript;

    // tried creating a struct which would hold the name of the outfit, the score given to it and the model/material which it applies to
    //struct S_OutfitStruct
    //{
    //    string m_outfitName;
    //    int m_appliedOutfitScore;
    //    material m_outfitMaterial;
    //
    //    public S_OutfitStruct(string name, int score, material mat)
    //    {
    //        m_outfitName = name;
    //        m_appliedOutfitScore = score;
    //        m_outfitMaterial = mat; 
    //    }
    //}

    // S_OutfitStruct[] s_Outfit = new S_OutfitStruct[]
    //{
    //    new S_OutfitStruct ("CASUAL", 1, m_outfitMats[0]),
    //    new S_OutfitStruct ("SMART_CASUAL", 3, m_outfitMats[1]),
    //    new S_OutfitStruct ("SMART", 2, m_outfitMats[2])
    //};


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

    // when an outfit on the UI is clicked
    void OutfitClicked(int buttonNo)
    {
        m_selectedOutfit = (e_Outfits)buttonNo;
        
        ig_Outfit[0].transform.parent.transform.parent.gameObject.SetActive(false);
        CheckSelectedModel();
        m_playerscript.enabled = true;
        m_cmScript.DisableCursor();    
    }

    // temporally made it so that the players camera is higher up in the scene (not via code)
    // this is also temp code for changing the material of the player to simulate the changing of the outfits and will be changed when we have more assets
    void CheckSelectedModel()
    {
        if (m_selectedOutfit == e_Outfits.CASUAL)
        {
            ChangeSelectedModel(m_outfitMats[0]);
            m_selectedOutfitScore = 1;
        }
        else if (m_selectedOutfit == e_Outfits.SMART_CASUAL)
        {
            ChangeSelectedModel(m_outfitMats[1]);
            m_selectedOutfitScore = 3;
        }
        else if (m_selectedOutfit == e_Outfits.SMART)
        {
            ChangeSelectedModel(m_outfitMats[2]);
            m_selectedOutfitScore = 2;
        }
        Debug.Log("Selected outfit is " + m_selectedOutfit);
        Debug.Log("Selected outfit score is " + m_selectedOutfitScore);
    }

    // will be changed to change the model/texture when the assets are created
    void ChangeSelectedModel(Material mat)
    {
        ig_Player.GetComponent<MeshRenderer>().material = mat;
    }

    // returns the score given to the outfit to future scenes
    public static int GetOutfitScore()
    {
        return m_selectedOutfitScore;
    }
}
