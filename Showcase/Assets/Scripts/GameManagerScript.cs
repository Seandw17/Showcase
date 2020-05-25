using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    //All of the HUDS should be added here so that they can be accessed in the editor
    [SerializeField]
    public GameObject ig_PlayerPanel;

    //This is the HUD that is displayed to the screen at all times
    GameObject ig_currenthud;

    // Start is called before the first frame update
    void Start()
    {
        //SetCurrentHUD(ig_PlayerPanel);
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
   

}
