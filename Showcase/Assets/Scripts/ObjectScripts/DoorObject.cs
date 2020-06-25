﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DoorObject : InteractableObjectBase
{
    bool m_dooropen = false;
    bool m_candoor = false;
    float m_rotationspeed = 50.0f;
    [SerializeField]
    string m_levelname;
    Scene scene;
   

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
      
        Debug.Log(scene.name);
    }

    // Update is called once per frame
    void Update()
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

    public override void Interact()
    {
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
                    m_gmscript.SetCurrentHUD(m_gmscript.ig_noResearchExitWarning);
                    StartCoroutine(FadeIn.AssetInOut(m_gmscript.ig_noResearchExitWarning.GetComponentInChildren<TextMeshProUGUI>(), 5.0f, 2.0f));
                    
                }
                else if(ConversationStore.IsOnlyNoneFlag().Equals(false))
                {
                    if (OutfitManager.GetOutfitScore() != 0)
                    {
                        LevelChange.ChangeLevel(m_levelname);
                    }
                    else
                    {
                        // Changed to call coroutine
                        m_gmscript.SetCurrentHUD(m_gmscript.ig_outfitExitWarning);
                        StartCoroutine(FadeIn.AssetInOut(m_gmscript.ig_outfitExitWarning.GetComponentInChildren<TextMeshProUGUI>(), 5.0f, 2.0f));
                    }
                }
            }
        }
        else
        {
            m_candoor = true;
            m_dooropen = !m_dooropen;
        } 
    }
}
