using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectionManager : MonoBehaviour
{
    //Reference to player controller
    PlayerController m_pcScript;

    //Reference to GameManagerScript
    GameManagerScript m_gmScript;

    //Reference to Cursour
    CursorController m_cmScript;

    //Mesh's for players choice
    [SerializeField] Mesh m_malemesh, m_femalemesh;

    //Array for materials for the player
    [SerializeField] Material[] m_playerracechoice;

    [SerializeField] GameObject m_playersexPanel, m_playermaleracePanel, m_playerfemaleracePanel;
     // Start is called before the first frame update
    void Start()
    {
        m_pcScript = FindObjectOfType<PlayerController>();
        m_cmScript = FindObjectOfType<CursorController>();
        m_gmScript = FindObjectOfType<GameManagerScript>();

        GameManagerScript.SetNewHUD(m_playersexPanel);
        m_cmScript.EnableCursor();

        m_pcScript.SetCanPlayerMove(false);
    }

    // Update is called once per frame
    void Update()
    {
        m_cmScript.EnableCursor();
    }

    public void SetPlayerMeshMale()
    {
        m_pcScript.SetPlayerMeshModel(m_malemesh);
        m_gmScript.m_isplayerSexchoiceone = true;

        GameManagerScript.SetNewHUD(m_playermaleracePanel);
    }

    public void SetPlayerMeshFemale()
    {
        m_pcScript.SetPlayerMeshModel(m_femalemesh);
        m_gmScript.m_isplayerSexchoiceone = false;

        GameManagerScript.SetNewHUD(m_playerfemaleracePanel);
    }

    public void SetRace1()
    {
        if (m_gmScript.m_isplayerSexchoiceone == true)
        {
            m_pcScript.SetPlayerMaterial(m_playerracechoice[0]);
        }
        else
        {
            m_pcScript.SetPlayerMaterial(m_playerracechoice[3]);
        }
        m_gmScript.m_playerracechoicebool[0] = true;
        LevelChange.ChangeLevel("ChooseOutfit");
    }

    public void SetRace2()
    {
        if (m_gmScript.m_isplayerSexchoiceone == true)
        {
            m_pcScript.SetPlayerMaterial(m_playerracechoice[1]);
        }
        else
        {
            m_pcScript.SetPlayerMaterial(m_playerracechoice[4]);
        }
        m_gmScript.m_playerracechoicebool[1] = true;
        LevelChange.ChangeLevel("ChooseOutfit");
    }

    public void SetRace3()
    {
        if (m_gmScript.m_isplayerSexchoiceone == true)
        {
            m_pcScript.SetPlayerMaterial(m_playerracechoice[2]);
        }
        else
        {
            m_pcScript.SetPlayerMaterial(m_playerracechoice[5]);
        }
        m_gmScript.m_playerracechoicebool[2] = true;
        LevelChange.ChangeLevel("ChooseOutfit");
    }
}
