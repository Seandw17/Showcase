using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum e_PanelTypes
{
    PLAYER,
    WARDROBE,
    LAPTOP,
    MAGAZINE,
    OUTFITWARNING,
    RESEARCHWARNING
}

public class GameManagerScript : MonoBehaviour
{
    //All of the HUDS should be added here so that they can be accessed in the editor
    [SerializeField]
    static private GameObject ig_PlayerPanel, ig_WardrobePanel, ig_LaptopPanel, ig_magazineWaitingRoom, ig_outfitExitWarning, ig_noResearchExitWarning;

    Text m_objectivetext;

    //This is the HUD that is displayed to the screen at all times
    static GameObject ig_currenthud;

    //Array of strings for the objective text
    [SerializeField]
    string[] m_objectivetextarray;

    int m_objectiveindex;

    static CursorController m_cmScript;

    // Start is called before the first frame update
    void Start()
    {
        //SetCurrentHUD(ig_PlayerPanel);
       
        m_cmScript = GetComponent<CursorController>();

        m_objectivetext = ig_PlayerPanel.GetComponent<Text>();
      
        DisplayObjectiveText();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void SetCurrentHUD(GameObject _CurrentHUD)
    {
        //Check if ig_currenthud is not null(Only time it should be is when the game starts)
        if (ig_currenthud != null)
        {
            //Remove the current HUD on screen
            ig_currenthud.SetActive(false);
        }
        //Set _currenthud to a new HUD
        ig_currenthud = _CurrentHUD;
        //Display the new HUD to the screen
        ig_currenthud.SetActive(true);
    }

    void DisplayObjectiveText()
    {
        m_objectivetext.text = m_objectivetextarray[m_objectiveindex];
    }

    public void IncrementObjectiveIndex()
    {
        m_objectiveindex++;
        DisplayObjectiveText();
    }

    public static CursorController GetCursor() => m_cmScript;

    /// <summary>
    /// Function to return panel
    /// </summary>
    /// <param name="_panel"></param>
    /// <returns></returns>
    static public GameObject ReturnPanel(e_PanelTypes _panel)
    {
        switch (_panel)
        {
            case e_PanelTypes.LAPTOP:
                return ig_LaptopPanel;
            case e_PanelTypes.MAGAZINE:
                return ig_magazineWaitingRoom;
            case e_PanelTypes.OUTFITWARNING:
                return ig_outfitExitWarning;
            case e_PanelTypes.RESEARCHWARNING:
                return ig_noResearchExitWarning;
            case e_PanelTypes.PLAYER:
                return ig_PlayerPanel;
            case e_PanelTypes.WARDROBE:
                return ig_WardrobePanel;
        }
        throw new System.Exception("Invalid value passed");
    }
    
}
