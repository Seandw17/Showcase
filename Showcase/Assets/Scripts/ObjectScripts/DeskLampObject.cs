﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskLampObject : InteractableObjectBase
{
    [SerializeField]
    Light m_light;

    bool m_lighton = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_light = FindObjectOfType<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        m_lighton = !m_lighton;
        if (m_lighton == true)
        {
            m_light.enabled = true;
        }
        else
        {
            m_light.enabled = false;
        }
    }
}