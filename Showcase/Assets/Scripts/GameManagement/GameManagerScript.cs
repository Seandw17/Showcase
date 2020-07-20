using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    //All of the HUDS should be added here so that they can be accessed in the editor
    [SerializeField] private GameObject ig_PlayerPanel; // KEEP
    [SerializeField] private GameObject ig_UIParent;


    static GameObject m_playerPanel, m_UIParent;

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
     int m_objectivesize = 9;
    static CursorController m_cmScript;

    //Bool to check if sex choice one was made
    public bool m_isplayerSexchoiceone;

    //Array of bools to check what race was chosen 0 = race1, 1 = race2, 2 = race3
    public bool[] m_playerracechoicebool;

    // Start is called before the first frame update
    void Start()
    {
        //SetCurrentHUD(ig_PlayerPanel);
       
        m_cmScript = GetComponent<CursorController>();

        m_playerPanel = ig_PlayerPanel; // KEEP
        m_UIParent = ig_UIParent;
        ig_currenthud = m_playerPanel;

        m_objectivetextarray = new string[m_objectivesize];
        m_objectiveboolarray = new bool[m_objectivesize];

        CreateTaskText();

        m_playerracechoicebool = new bool[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Set a new HUD
    /// </summary>
    /// <param name="_new">the new HUD</param>
    static public void SetNewHUD(GameObject _new)
    {
        Debug.Assert(_new.GetComponent<CanvasRenderer>() != null,
            "Object passed to HUD must be a UI (Canvas) Object");

        Debug.Log(_new.name);
        if (ig_currenthud != null)
        {
            ig_currenthud.SetActive(false);
        }
        ig_currenthud = _new;
        ig_currenthud.transform.parent = m_UIParent.transform;
        ig_currenthud.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        ig_currenthud.SetActive(true);
    }

    /// <summary>
    /// Function to force the current HUD to null
    /// </summary>
    static public void SetHUDToNull()
    {
        ig_currenthud.SetActive(false);
        if (ig_currenthud != m_playerPanel)
        {
            ig_currenthud.transform.parent = null;
            SceneManager.MoveGameObjectToScene(ig_currenthud,
                SceneManager.GetActiveScene());
        }
        ig_currenthud = null;
    }

    /// <summary>
    /// Restore the HUD to the player panel
    /// </summary>
    static public void SetHUDBack()
    {
        Debug.Log("Setting hud back");
        if (ig_currenthud != null)
        {
            ig_currenthud.SetActive(false);
            ig_currenthud.transform.parent = null;
            SceneManager.MoveGameObjectToScene(ig_currenthud,
                SceneManager.GetActiveScene());
        }
        ig_currenthud = m_playerPanel;
        ig_currenthud.SetActive(true);
    }

    static public GameObject ReturnCurrentHUD() => ig_currenthud;

    

    void DisplayObjectiveText()
    {
       m_objectivetext.text = m_objectivetextarray[m_objectiveindex];
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
        m_objectivetextarray[0] = "Do Some Research\nFor Your Interview\n";
        m_objectivetextarray[1] = "Pick An Appropriate Outfit";
        m_objectivetextarray[2] = "Leave For Your Interview";
        m_objectivetextarray[3] = "Speak With A Staff Member";
        m_objectivetextarray[4] = "Follow Staff Member";
        m_objectivetextarray[5] = "Take A Seat";
        m_objectivetextarray[6] = "Wait To Be Called In";
        m_objectivetextarray[7] = "Go In For Your Interview";
        m_objectivetextarray[8] = "Complete The Interview";
        DisplayObjectiveText();
    }

    public static CursorController GetCursor() => m_cmScript;

    static public void UIActive(bool _state)
    {
        Debug.Log("TWAS I");
        ig_currenthud.SetActive(_state);
    }
}
