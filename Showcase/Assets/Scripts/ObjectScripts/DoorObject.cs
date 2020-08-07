using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorObject : InteractableObjectBase
{
    bool m_dooropen = false;
    bool m_candoor = false;
    [SerializeField] private bool m_lockDoor = false;
    float m_rotationspeed = 50.0f;
    [SerializeField]
    string m_levelname;
    Scene scene;

    [SerializeField] GameObject m_researchWarning, m_outfitWarning;
   

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_lockDoor)
        {
            if (m_candoor == true)
            {
                if (m_dooropen == true)
                {
                    if (transform.rotation.z <= 0.5) //some reason this stops the rotation at 90 on the z axis so dont touch
                    {
                        transform.Rotate(Vector3.forward * m_rotationspeed * Time.deltaTime);
                        

                    }

                }
                else if (m_dooropen == false)
                {
                    if (transform.rotation.z >= 0)
                    {
                        transform.Rotate(Vector3.back * m_rotationspeed * Time.deltaTime);
                    }

                }
            }
        }
       
    }

    public override void Interact()
    {
        // - play sound when doors are opening
        // -play sound when doors are closing
       scene = SceneManager.GetActiveScene();
        if (scene.name.Equals("ChooseOutfit"))
        {
            if (m_levelname.Equals(""))
            {

            }
            else
            {
                if (ConversationStore.IsOnlyNoneFlag().Equals(true))
                {
                    Debug.Log("You need to do some research fool");

                    // Changed to call coroutine
                    GameManagerScript.SetNewHUD(m_researchWarning);
                    StartCoroutine(FadeIn.AssetInOut(GameManagerScript
                        .ReturnCurrentHUD().GetComponentInChildren
                        <TextMeshProUGUI>(), 5, 2));
                }
                else if(ConversationStore.IsOnlyNoneFlag().Equals(false))
                {
                    if (OutfitManager.GetOutfitScore() != 0)
                    {
                        LevelChange.ChangeLevel(m_levelname);
                        FMODUnity.RuntimeManager.PlayOneShot("event:/SpotEffects/door_close");
                        m_gmscript.SetTaskTrue(2);
                    }
                    else
                    {
                        // Changed to call coroutine
                        GameManagerScript.SetNewHUD(m_outfitWarning);
                        StartCoroutine(FadeIn.AssetInOut(GameManagerScript
                            .ReturnCurrentHUD().GetComponentInChildren
                            <TextMeshProUGUI>(), 5, 2));
                    }
                }
            }
        }
        else
        {
            m_candoor = true;
            m_dooropen = !m_dooropen;
            if (m_dooropen == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SpotEffects/door_open");
            }
            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/SpotEffects/door_close");
            }
            if(gameObject.scene.name == "Cafe")
            {
                LevelChange.ChangeLevel(m_levelname);
            }
        } 
    }

    /// <summary>
    /// returns the value of the door in order to know if it was open
    /// </summary>
    /// <returns></returns>
    public bool Getm_dooropen()
    {
        return m_dooropen;
    }

    public void Setm_dooropen(bool _state)
    {
        m_dooropen = _state;
    }

    public void Setm_dooropenApplicant(bool _state)
    {
        m_candoor = _state;
        m_dooropen = _state;
    }

    /// <summary>
    /// Changes the state of the door lock or unlock
    /// </summary>
    /// <param name="_state"></param>
    public void SetM_lockDoor(bool _state)
    {
        m_lockDoor = _state;
    }
}
