using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    //All of the HUDS should be added here so that they can be accessed in the editor
    public GameObject ig_PlayerPanel, ig_WardrobePanel, ig_LaptopPanel, ig_magazineWaitingRoom, ig_outfitExitWarning, ig_noResearchExitWarning;

    [SerializeField]
    Text m_objectivetext;

    //This is the HUD that is displayed to the screen at all times
    GameObject ig_currenthud;

    //Array of strings for the objective text
    [SerializeField]
    string[] m_objectivetextarray;

    int m_objectiveindex;

    public CursorController m_cmScript;

    // Start is called before the first frame update
    void Start()
    {
        //SetCurrentHUD(ig_PlayerPanel);
       
        m_cmScript = GetComponent<CursorController>();

      
        DisplayObjectiveText();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentHUD(GameObject _CurrentHUD)
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
}
