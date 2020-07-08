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

    [SerializeField]
    Material[] m_outfitMats;

    //protected PlayerController m_playerscript;
    CursorController m_cmScript;

    GameManagerScript m_gmscript;

    // Start is called before the first frame update
    void Start()
    {
        //ig_Outfit = GameObject.FindGameObjectsWithTag("Outfit");

        m_cmScript = GetComponent<CursorController>();
        m_gmscript = FindObjectOfType<GameManagerScript>();

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
        GameObject.Find("Player").GetComponent<MeshRenderer>().material = mat;
    }

    // returns the score given to the outfit to future scenes
    public static int GetOutfitScore()
    {
        return m_selectedOutfitScore;
    }
}
