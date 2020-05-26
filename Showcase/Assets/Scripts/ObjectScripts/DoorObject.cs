using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : InteractableObjectBase
{
    bool m_dooropen = false;
    float m_rotationspeed = 1.0f;
    Quaternion m_opendoorrotation;
    Quaternion m_closedoorrotation;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_opendoorrotation = Quaternion.AngleAxis(90.0f, transform.up);
        m_closedoorrotation = Quaternion.AngleAxis(0.0f, transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_dooropen == true)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,m_closedoorrotation , m_rotationspeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,m_opendoorrotation , m_rotationspeed * Time.deltaTime);
        }
    }

    public override void Interact()
    {
        m_dooropen = !m_dooropen;
        
    }
}
