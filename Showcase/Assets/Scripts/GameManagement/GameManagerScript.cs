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
    RESEARCHWARNING,
    WEBSITEMAIN,
    WEBSITEPRODUCT,
    WEBSITEABOUT,
    WEBSITECOMMUNITY,
    WEBSITELOOK
}

public class GameManagerScript : MonoBehaviour
{
    //All of the HUDS should be added here so that they can be accessed in the editor
    [SerializeField] private GameObject ig_PlayerPanel;
    [SerializeField] private GameObject ig_WardrobePanel;
    [SerializeField] private GameObject ig_LaptopPanel;
    [SerializeField] private GameObject ig_magazineWaitingRoom;
    [SerializeField] private GameObject ig_outfitExitWarning;
    [SerializeField] private GameObject ig_noResearchExitWarning;
    [SerializeField] private GameObject ig_WebsiteMainPanel;
    [SerializeField] private GameObject ig_WebsiteProductPanel;
    [SerializeField] private GameObject ig_WebsiteAboutPanel;
    [SerializeField] private GameObject ig_WebsiteCommunityPanel;
    [SerializeField] private GameObject ig_WebsiteLookPanel;

    static GameObject m_playerPanel, m_WardrobePanel, m_laptopPanel, m_magazinePanel, m_outfitWarnPanel, m_researchWarnPanel, m_websitemainPanel, m_websiteproductPanel, m_websiteaboutPanel, m_websitecommunityPanel, m_websitelookPanel;

    [SerializeField] Text m_objectivetext;

    //This is the HUD that is displayed to the screen at all times
    static GameObject ig_currenthud;

    //Array of strings for the objective text
    [SerializeField] string[] m_objectivetextarray;

    //Array of bools for the objectives
    public bool[] m_objectiveboolarray;
    // the index number for the array
     int m_objectiveindex = 0;
    //The size of the array
     int m_objectivesize = 8;
    static CursorController m_cmScript;

    // Start is called before the first frame update
    void Start()
    {
        //SetCurrentHUD(ig_PlayerPanel);
       
        m_cmScript = GetComponent<CursorController>();

        m_playerPanel = ig_PlayerPanel;
        m_WardrobePanel = ig_WardrobePanel;
        m_laptopPanel = ig_LaptopPanel;
        m_magazinePanel = ig_magazineWaitingRoom;
        m_outfitWarnPanel = ig_outfitExitWarning;
        m_researchWarnPanel = ig_noResearchExitWarning;

        m_websitemainPanel = ig_WebsiteMainPanel;
        m_websiteproductPanel = ig_WebsiteProductPanel;
        m_websiteaboutPanel = ig_WebsiteAboutPanel;
        m_websitecommunityPanel = ig_WebsiteCommunityPanel;
        m_websitelookPanel = ig_WebsiteLookPanel;

        ig_currenthud = m_playerPanel;

        m_objectivetextarray = new string[m_objectivesize];
        m_objectiveboolarray = new bool[m_objectivesize];

        CreateTaskText();
        
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void SetCurrentHUD(GameObject _CurrentHUD)
    {
        Debug.Log(_CurrentHUD.name);
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
       m_objectivetext.text = "Objective: " + m_objectivetextarray[m_objectiveindex];
    }

    public void SetTaskTrue(int _taskindex)
    {
        m_objectiveboolarray[_taskindex] = true;
        IncrementObjectiveIndex();
    }

     public void IncrementObjectiveIndex()
     {
        m_objectiveindex++;
        DisplayObjectiveText();
     }

    void CreateTaskText()
    {
        m_objectivetextarray[0] = "Do Some Research For Your Interview";
        m_objectivetextarray[1] = "Pick An Appropriate Outfit";
        m_objectivetextarray[2] = "Leave For Your Interview";
        m_objectivetextarray[3] = "Speak With A Staff Member";
        m_objectivetextarray[4] = "Follow Staff Member";
        m_objectivetextarray[5] = "Take A Seat And Wait To Be Called In";
        m_objectivetextarray[6] = "Go In For Your Interview";
        m_objectivetextarray[7] = "Complete The Interview";
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
                return m_laptopPanel;
            case e_PanelTypes.MAGAZINE:
                return m_magazinePanel;
            case e_PanelTypes.OUTFITWARNING:
                return m_outfitWarnPanel;
            case e_PanelTypes.RESEARCHWARNING:
                return m_researchWarnPanel;
            case e_PanelTypes.PLAYER:
                return m_playerPanel;
            case e_PanelTypes.WARDROBE:
                return m_WardrobePanel;
            case e_PanelTypes.WEBSITEMAIN:
                return m_websitemainPanel;
            case e_PanelTypes.WEBSITEPRODUCT:
                return m_websiteproductPanel;
            case e_PanelTypes.WEBSITEABOUT:
                return m_websiteaboutPanel;
            case e_PanelTypes.WEBSITECOMMUNITY:
                return m_websitecommunityPanel;
            case e_PanelTypes.WEBSITELOOK:
                return m_websitelookPanel;
        }
        throw new System.Exception("Invalid value passed");
    }

    static public void UIActive(bool _state)
    {
        Debug.Log("TWAS I");
        ig_currenthud.SetActive(_state);
    }
}
