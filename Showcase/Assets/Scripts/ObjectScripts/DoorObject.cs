using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                if (ConversationStore.IsOnlyNoneFlag().Equals(false))
                {
                    Debug.Log("You need to do some research fool");
                    //BIG SEAN ADD SOME CODE HERE FOR THE PLAYER WHEN THEY HAVE NOT DONE ANY RESEARCH. LAZY BASTARDS
                }
                else if(ConversationStore.IsOnlyNoneFlag().Equals(true))
                {
                    if (OutfitManager.GetOutfitScore() != 0)
                    {
                        LevelChange.ChangeLevel(m_levelname);
                    }
                    else
                    {
                        //BIG SEAN ADD SOME CODE HERE FOR THE PLAYER WHEN THEY HAVE NOT SELECTED AN OUTFIT. DIRTY SCOUNDRAL 
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
