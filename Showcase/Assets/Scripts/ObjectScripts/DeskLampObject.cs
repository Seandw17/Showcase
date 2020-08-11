using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskLampObject : InteractableObjectBase
{
    [SerializeField]
    List<Light> m_light;
    

    bool m_lighton = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        //m_light = FindObjectOfType<Light>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        m_lighton = !m_lighton;
        if (m_lighton == false)
        {
            for (int i = 0; i < m_light.Count; i++)
            {
                m_light[i].enabled = true;
            }
            

            FMODUnity.RuntimeManager.PlayOneShot("event:/lamp_switch", GetComponent<Transform>().position);
            GetComponent<FMODUnity.StudioEventEmitter>().Play();
        }
        else
        {
            for (int i = 0; i < m_light.Count; i++)
            {
                m_light[i].enabled = false;
            }

            FMODUnity.RuntimeManager.PlayOneShot("event:/lamp_switch", GetComponent<Transform>().position);
            GetComponent<FMODUnity.StudioEventEmitter>().Stop();
        }
    }
}
