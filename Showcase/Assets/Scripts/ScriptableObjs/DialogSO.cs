﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Dialog", order = 1)]
public class DialogSO : ScriptableObject
{
    public string name;
    public List<SenteceSOSupport> sentences;

}
