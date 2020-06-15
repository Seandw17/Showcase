using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : InteractableObjectBase
{
    bool m_dooropen = false;
    float m_rotationspeed = 50.0f;
   
   

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_dooropen == true)
        {
            if (transform.rotation.z <= 0.5) //some reason this stops the rotation at 90 on the z axis so dont touch
            {
                transform.Rotate(Vector3.forward * m_rotationspeed * Time.deltaTime);
                
            }
            
        }
        else if(m_dooropen == false)
        {
            if (transform.rotation.z >= 0)
            {
                transform.Rotate(Vector3.back * m_rotationspeed * Time.deltaTime);
            }
     
        }
    }

    public override void Interact()
    {
        m_dooropen = !m_dooropen;
        
    }
}
