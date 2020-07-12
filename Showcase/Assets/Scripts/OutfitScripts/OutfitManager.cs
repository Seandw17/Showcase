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

    public GameObject[] ig_Outfit;

    //GameObject ig_Player;

    // holds the materials/models for each race and gender    
    [SerializeField]
    Material[] m_currentOutfitMats, m_maleRace1OutfitMats, m_maleRace2OutfitMats, m_maleRace3OutfitMats, m_femaleRace1OutfitMats, m_femaleRace2OutfitMats, m_femaleRace3OutfitMats;

    //protected PlayerController m_playerscript;
    CursorController m_cmScript;

    GameManagerScript m_gmscript;

    PlayerController m_pcScript;

    // Start is called before the first frame update
    void Start()
    {
        //ig_Outfit = GameObject.FindGameObjectsWithTag("Outfit");

        m_cmScript = GetComponent<CursorController>();
        m_gmscript = FindObjectOfType<GameManagerScript>();
        m_pcScript = FindObjectOfType<PlayerController>();

        for (int i = 0; i < ig_Outfit.Length; i++)
        {
            Button m_tempButton = ig_Outfit[i].GetComponent<Button>();
            int m_tempi = i;
            m_tempButton.onClick.AddListener(() => OutfitClicked(m_tempi));
        }
        
        m_currentOutfitMats = new Material[3];
        SetOutfitsOnLoad();

        ig_Outfit[0].transform.parent.transform.parent.gameObject.SetActive(false);
        GameManagerScript.SetHUDBack();
    }

    // depending on what race and gender the player chooses, change the selected outfits
    void SetOutfitsOnLoad()
    { 
        Debug.Log("Material's being assigned");
        // if male
        if (m_gmscript.m_isplayerSexchoiceone)
        {
            // check which race 
            if (m_gmscript.m_playerracechoicebool[0])
            {
                m_currentOutfitMats[0] = m_maleRace1OutfitMats[0];
                m_currentOutfitMats[1] = m_maleRace1OutfitMats[1];
                m_currentOutfitMats[2] = m_maleRace1OutfitMats[2];
                Debug.Log("Player is male with race 1");
            }
            else if (m_gmscript.m_playerracechoicebool[1])
            {
                m_currentOutfitMats[0] = m_maleRace2OutfitMats[0];
                m_currentOutfitMats[1] = m_maleRace2OutfitMats[1];
                m_currentOutfitMats[2] = m_maleRace2OutfitMats[2];
                Debug.Log("Player is male with race 2");
            }
            else if (m_gmscript.m_playerracechoicebool[2])
            {
                m_currentOutfitMats[0] = m_maleRace3OutfitMats[0];
                m_currentOutfitMats[1] = m_maleRace3OutfitMats[1];
                m_currentOutfitMats[2] = m_maleRace3OutfitMats[2];
                Debug.Log("Player is male with race 3");
            }
        }
        // if female
        else if (!m_gmscript.m_isplayerSexchoiceone)
        {
            // check which race 
            if (m_gmscript.m_playerracechoicebool[0])
            {
                m_currentOutfitMats[0] = m_femaleRace1OutfitMats[0];
                m_currentOutfitMats[1] = m_femaleRace1OutfitMats[1];
                m_currentOutfitMats[2] = m_femaleRace1OutfitMats[2];
                Debug.Log("Player is female with race 1");
            }
            else if (m_gmscript.m_playerracechoicebool[1])
            {
                m_currentOutfitMats[0] = m_femaleRace2OutfitMats[0];
                m_currentOutfitMats[1] = m_femaleRace2OutfitMats[1];
                m_currentOutfitMats[2] = m_femaleRace2OutfitMats[2];
                Debug.Log("Player is female with race 2");
            }
            else if (m_gmscript.m_playerracechoicebool[2])
            {
                m_currentOutfitMats[0] = m_femaleRace3OutfitMats[0];
                m_currentOutfitMats[1] = m_femaleRace3OutfitMats[1];
                m_currentOutfitMats[2] = m_femaleRace3OutfitMats[2];
                Debug.Log("Player is female with race 3");
            }
        }
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
        FindObjectOfType<PlayerController>().enabled = true;
        GameManagerScript.SetHUDBack();
        m_cmScript.DisableCursor();
        FMODUnity.RuntimeManager.PlayOneShot("event:/SpotEffects/Bedroom/Wardrobe/collect_clothes");
        m_gmscript.SetTaskTrue(1);
    }

    // temporally made it so that the players camera is higher up in the scene (not via code)
    // this is also temp code for changing the material of the player to simulate the changing of the outfits and will be changed when we have more assets
    void CheckSelectedModel()
    {
        if (m_selectedOutfit == e_Outfits.CASUAL)
        {
            ChangeSelectedModel(m_currentOutfitMats[0]);
            m_selectedOutfitScore = 1;
        }
        else if (m_selectedOutfit == e_Outfits.SMART_CASUAL)
        {
            ChangeSelectedModel(m_currentOutfitMats[1]);
            m_selectedOutfitScore = 3;
        }
        else if (m_selectedOutfit == e_Outfits.SMART)
        {
            ChangeSelectedModel(m_currentOutfitMats[2]);
            m_selectedOutfitScore = 2;
        }
        Debug.Log("Selected outfit is " + m_selectedOutfit);
        Debug.Log("Selected outfit score is " + m_selectedOutfitScore);
    }

    // will be changed to change the model/texture when the assets are created
    void ChangeSelectedModel(Material mat)
    {
        //GameObject.Find("Player").GetComponent<MeshRenderer>().material = mat;
        m_pcScript.SetPlayerMaterial(mat);
    }

    // returns the score given to the outfit to future scenes
    public static int GetOutfitScore()
    {
        return m_selectedOutfitScore;
    }
}
